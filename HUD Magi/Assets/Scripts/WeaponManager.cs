using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public AbilityManager abilityManager;

    public WeaponType currentWeapon = WeaponType.Magic;

    public void SetWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Weapon changed to: " + currentWeapon);

        if (abilityManager != null)
            abilityManager.OnWeaponChanged();
    }


    public WeaponType GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
