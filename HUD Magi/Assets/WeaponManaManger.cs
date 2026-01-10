using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WeaponManaManager : MonoBehaviour
{
    public WeaponManager weaponManager;
    public WeaponManaData[] weaponMana;
    public event Action<WeaponType, int> OnManaSpent;


    public int GetCurrentMana(WeaponType weapon)
    {
        WeaponManaData data = GetData(weapon);
        return data != null ? data.currentMana : 0;
    }

    public int GetMaxMana(WeaponType weapon)
    {
        WeaponManaData data = GetData(weapon);
        return data != null ? data.maxMana : 0;
    }

    void Start()
    {
        for (int i = 0; i < weaponMana.Length; i++)
        {
            weaponMana[i].currentMana = weaponMana[i].maxMana;
        }
    }

    void Update()
    {
        RegenerateMana();
    }

    void RegenerateMana()
    {
        WeaponType activeWeapon = weaponManager.GetCurrentWeapon();

        for (int i = 0; i < weaponMana.Length; i++)
        {
            WeaponManaData data = weaponMana[i];

            if (data.currentMana >= data.maxMana)
                continue;

            float regenRate =
                data.weaponType == activeWeapon
                ? data.regenActive
                : data.regenInactive;

            data.regenBuffer += regenRate * Time.deltaTime;

            if (data.regenBuffer >= 1f)
            {
                int gained = Mathf.FloorToInt(data.regenBuffer);
                data.regenBuffer -= gained;

                data.currentMana += gained;
                data.currentMana = Mathf.Clamp(data.currentMana, 0, data.maxMana);
            }
        }

    }


    public float GetRegenRate(WeaponType weapon)
    {
     WeaponManaData data = GetData(weapon);
     if (data == null) return 0f;
     bool isActive = weaponManager.GetCurrentWeapon() == weapon;
      return isActive? data.regenActive : data.regenInactive;
    }



WeaponManaData GetData(WeaponType weapon)
    {
        for (int i = 0; i < weaponMana.Length; i++)
        {
            if (weaponMana[i].weaponType == weapon)
                return weaponMana[i];
        }
        return null;
    }

    public bool HasEnoughMana(int cost)
    {
        WeaponManaData data = GetData(weaponManager.GetCurrentWeapon());
        return data != null && data.currentMana >= cost;
    }

    public void UseMana(int cost)
    {
        Debug.Log($"UseMana CALLED | cost = {cost} | frame = {Time.frameCount}");

        WeaponManaData data = GetData(weaponManager.GetCurrentWeapon());
        if (data == null) return;

        data.currentMana -= cost;
        data.currentMana = Mathf.Clamp(data.currentMana, 0, data.maxMana);

        // 🔔 Fortæl UI at mana blev brugt
        OnManaSpent?.Invoke(data.weaponType, cost);

    }


    public int GetCurrentMana()
    {
        WeaponManaData data = GetData(weaponManager.GetCurrentWeapon());
        return data != null ? data.currentMana : 0;
    }

    public int GetMaxMana()
    {
        WeaponManaData data = GetData(weaponManager.GetCurrentWeapon());
        return data != null ? data.maxMana : 0;
    }

    public float GetCurrentManaFloat(WeaponType weapon)
    {
        WeaponManaData data = GetData(weapon);
        return data != null ? data.currentMana : 0f;
    }

    


}
