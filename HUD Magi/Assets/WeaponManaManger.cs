using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManaManager : MonoBehaviour
{
    public WeaponManager weaponManager;
    public WeaponManaData[] weaponManaData;

    void Start()
    {
        for (int i = 0; i < weaponManaData.Length; i++)
        {
            weaponManaData[i].currentMana = weaponManaData[i].MaxMana;
        }
    }

    WeaponManaData GetCurrentWeaponMana()
    {
        WeaponType current = weaponManager.GetCurrentWeapon();

        for (int i = 0; i < weaponManaData.Length; i++)
        {
            if (weaponManaData[i].weaponType == current)
                return weaponManaData[i];
        }

        return null;
    }

    public bool HasEnoughMana(float cost)
    {
        WeaponManaData data = GetCurrentWeaponMana();
        return data != null && data.currentMana >= cost;
    }

    public void UseMana(float cost)
    {
        WeaponManaData data = GetCurrentWeaponMana();
        if (data == null) return;

        data.currentMana -= cost;
        if (data.currentMana < 0f)
            data.currentMana = 0f;
    }

    public float GetManaPercent()
    {
        WeaponManaData data = GetCurrentWeaponMana();
        if (data == null) return 0f;

        return data.currentMana / data.MaxMana;
    }

    public int GetSegmentCount()
    {
        WeaponManaData data = GetCurrentWeaponMana();
        return data != null ? data.manaSegments : 0;
    }
}
