using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    AbilityCooldown[] magicCooldowns = new AbilityCooldown[4];
    AbilityCooldown[] gunCooldowns = new AbilityCooldown[4];
    AbilityCooldown[] swordCooldowns = new AbilityCooldown[4];

    AbilityCooldown magicUltCooldown;
    AbilityCooldown gunUltCooldown;
    AbilityCooldown swordUltCooldown;

    public IndependentManaRadialBar magicManaBar;
    public IndependentManaRadialBar gunManaBar;
    public IndependentManaRadialBar swordManaBar;

    public UltUIIndicator magicUltUI;
    public UltUIIndicator gunUltUI;
    public UltUIIndicator swordUltUI;

    [Header("Ult abilities")]
    public AbilityData magicUlt;
    public AbilityData gunUlt;
    public AbilityData swordUlt;

    public KeyCode ultKey = KeyCode.R;

    public WeaponManager weaponManager;
    public WeaponManaManager manaManager;
    public AbilityWheelUI abilityWheelUI;

    public AbilityData[] magicAbilities = new AbilityData[4];
    public AbilityData[] gunAbilities = new AbilityData[4];
    public AbilityData[] swordAbilities = new AbilityData[4];

    private int selectedAbilityIndex = -1;

    void Start()
    {
        InitCooldowns(magicAbilities, ref magicCooldowns);
        InitCooldowns(gunAbilities, ref gunCooldowns);
        InitCooldowns(swordAbilities, ref swordCooldowns);

        magicUltCooldown = new AbilityCooldown(magicUlt.cooldownTime);
        gunUltCooldown = new AbilityCooldown(gunUlt.cooldownTime);
        swordUltCooldown = new AbilityCooldown(swordUlt.cooldownTime);

        abilityWheelUI.SetWeapon(weaponManager.GetCurrentWeapon());
    }
    void InitCooldowns(AbilityData[] abilities, ref AbilityCooldown[] cooldowns)
    {
        for (int i = 0; i < abilities.Length; i++)
            cooldowns[i] = new AbilityCooldown(abilities[i].cooldownTime);
    }

    void Update()
    {
        HandleInput();
        UpdateCooldowns();
        UpdateUltUI();
        UpdateCooldownUI(); // 🔥 MANGLER
    }

    void UpdateCooldowns()
    {
        UpdateSet(magicCooldowns);
        UpdateSet(gunCooldowns);
        UpdateSet(swordCooldowns);
    }
    void UpdateSet(AbilityCooldown[] set)
    {
        foreach (var cd in set)
            cd.Update(Time.deltaTime);
    }

    void UpdateUltUI()
    {
        magicUltUI.SetReady(magicManaBar.IsFull());
        gunUltUI.SetReady(gunManaBar.IsFull());
        swordUltUI.SetReady(swordManaBar.IsFull());
    }

    void HandleInput()
    {
        // Keyboard 1–4
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectAbility(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectAbility(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectAbility(2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectAbility(3);

        // Scroll wheel (ability skift)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            ScrollAbility(scroll);
        }

        // Brug ability
        if (Input.GetMouseButtonDown(0))
            UseSelectedAbility();

        if (Input.GetKeyDown(ultKey))
        {
            TryUseUlt();
        }

    }
    void ScrollAbility(float scrollInput)
    {
        int abilityCount = 4;

        if (selectedAbilityIndex < 0)
            selectedAbilityIndex = 0;

        if (scrollInput > 0f)
            selectedAbilityIndex--;
        else if (scrollInput < 0f)
            selectedAbilityIndex++;

        // Wrap-around
        if (selectedAbilityIndex < 0)
            selectedAbilityIndex = abilityCount - 1;

        if (selectedAbilityIndex >= abilityCount)
            selectedAbilityIndex = 0;

        SelectAbility(selectedAbilityIndex);
    }


    void SelectAbility(int index)
    {
        selectedAbilityIndex = index;

        AbilityData ability = GetCurrentAbility(index);
        if (ability != null)
            Debug.Log("Selected ability: " + ability.abilityName);

        // 🔑 Fortæl UI hvilken ability der er valgt
        abilityWheelUI.SetSelectedAbility(index);
    }

    void UseSelectedAbility()
    {
        if (selectedAbilityIndex < 0)
            return;

        AbilityData ability = GetCurrentAbility(selectedAbilityIndex);
        if (ability == null)
            return;

        AbilityCooldown cd = GetCurrentCooldown(selectedAbilityIndex);

        // ❌ Bloker hvis cooldown
        if (!cd.IsReady())
        {
            Debug.Log("Ability on cooldown");
            return;
        }

        // ❌ Bloker hvis ikke nok mana
        if (!manaManager.HasEnoughMana(ability.manaCost))
        {
            Debug.Log("Not enough mana");
            return;
        }

        // ✅ Brug mana
        manaManager.UseMana(ability.manaCost);

        // ✅ Start cooldown
        cd.Start();

        Debug.Log("USED ability: " + ability.abilityName);
    }

    AbilityCooldown GetCurrentCooldown(int index)
    {
        WeaponType weapon = weaponManager.GetCurrentWeapon();

        if (weapon == WeaponType.Magic) return magicCooldowns[index];
        if (weapon == WeaponType.Gun) return gunCooldowns[index];
        if (weapon == WeaponType.Sword) return swordCooldowns[index];

        return null;
    }
    AbilityData GetCurrentAbility(int index)
    {
        WeaponType weapon = weaponManager.GetCurrentWeapon();

        if (weapon == WeaponType.Magic)
            return magicAbilities[index];

        if (weapon == WeaponType.Gun)
            return gunAbilities[index];

        if (weapon == WeaponType.Sword)
            return swordAbilities[index];

        return null;
    }

    // 🔔 KALDES FRA WeaponManager når våben skiftes
    public void OnWeaponChanged()
    {
        abilityWheelUI.SetWeapon(weaponManager.GetCurrentWeapon());
    }
    void TryUseUlt()
    {
        WeaponType weapon = weaponManager.GetCurrentWeapon();

        IndependentManaRadialBar bar = GetManaBarForWeapon(weapon);
        AbilityData ult = GetUltForWeapon(weapon);
        AbilityCooldown ultCooldown = GetUltCooldown(weapon);

        if (bar == null || ult == null || ultCooldown == null)
            return;

        // ❌ Ult er ikke klar
        if (!bar.IsFull() || !ultCooldown.IsReady())
            return;

        // 🔥 HER UDFØRES ULT-EFFEKTEN
        Debug.Log("ULT USED: " + ult.abilityName);

        // 🔑 TØM GAMEPLAY-MANA (vigtigt!)
        manaManager.ConsumeAllMana(weapon);

        // 🕒 🔥 TRIN 7: START ULT-COOLDOWN
        ultCooldown.Start();
    }

    IndependentManaRadialBar GetManaBarForWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Magic: return magicManaBar;
            case WeaponType.Gun: return gunManaBar;
            case WeaponType.Sword: return swordManaBar;
        }
        return null;
    }

    AbilityData GetUltForWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Magic: return magicUlt;
            case WeaponType.Gun: return gunUlt;
            case WeaponType.Sword: return swordUlt;
        }
        return null;
    }

    AbilityCooldown GetUltCooldown(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Magic:
                return magicUltCooldown;

            case WeaponType.Gun:
                return gunUltCooldown;

            case WeaponType.Sword:
                return swordUltCooldown;
        }

        return null;
    }
    void UpdateCooldownUI()
    {
        WeaponType weapon = weaponManager.GetCurrentWeapon();

        if (weapon == WeaponType.Magic)
            abilityWheelUI.UpdateCooldownVisuals(weapon, magicCooldowns);

        if (weapon == WeaponType.Gun)
            abilityWheelUI.UpdateCooldownVisuals(weapon, gunCooldowns);

        if (weapon == WeaponType.Sword)
            abilityWheelUI.UpdateCooldownVisuals(weapon, swordCooldowns);
    }

}
