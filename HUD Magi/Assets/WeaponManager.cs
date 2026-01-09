using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponType currentWeapon = WeaponType.Magic;

    public void SetWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Weapon changed to: " + currentWeapon);
    }

    public WeaponType GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
