using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public WeaponManager weaponManager;
    public WeaponManaManager manaManager;
    public IndependentManaRadialBar radialBar;

    public AbilityData[] magicAbilities = new AbilityData[4];
    public AbilityData[] gunAbilities = new AbilityData[4];
    public AbilityData[] swordAbilities = new AbilityData[4];

    private int selectedAbilityIndex = 0;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectAbility(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectAbility(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectAbility(2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectAbility(3);

        if (Input.GetMouseButtonDown(0))
        {
            UseSelectedAbility();
        }

    }

    void SelectAbility(int index)
    {
        selectedAbilityIndex = index;

        AbilityData ability = GetCurrentAbility(index);

        if (ability != null)
        {
            Debug.Log("Selected ability: " + ability.abilityName);
        }
    }

    void UseSelectedAbility()
    {
        AbilityData ability = GetCurrentAbility(selectedAbilityIndex);

        if (ability == null)
            return;

        if (!manaManager.HasEnoughMana(ability.manaCost))
        {
            Debug.Log("Not enough mana");
            return;
        }

        manaManager.UseMana(ability.manaCost);


        Debug.Log(
            "USED ability: " + ability.abilityName +
            " | Mana left: " + manaManager.GetCurrentMana()
        );


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
    public void UseMana(int cost)
    {
        Debug.Log($"UseMana called: {cost}");

    }

}
