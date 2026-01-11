using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public WeaponManager weaponManager;
    public WeaponManaManager manaManager;
    public AbilityWheelUI abilityWheelUI;

    public AbilityData[] magicAbilities = new AbilityData[4];
    public AbilityData[] gunAbilities = new AbilityData[4];
    public AbilityData[] swordAbilities = new AbilityData[4];

    private int selectedAbilityIndex = -1;

    void Start()
    {
        // Sørg for at UI starter korrekt
        abilityWheelUI.SetWeapon(weaponManager.GetCurrentWeapon());
    }

    void Update()
    {
        HandleInput();
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

        if (!manaManager.HasEnoughMana(ability.manaCost))
        {
            Debug.Log("Not enough mana");
            return;
        }

        // 🔑 Brug mana ÉT sted
        manaManager.UseMana(ability.manaCost);

        Debug.Log("USED ability: " + ability.abilityName);
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
}
