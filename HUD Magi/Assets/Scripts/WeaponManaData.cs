using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponManaData
{
    public WeaponType weaponType;

    public int maxMana = 6;
    public int currentMana;


    public float regenActive = 3f;
    public float regenInactive = 0.5f;

    [HideInInspector]
    public float regenBuffer; // samler regen over tid

    public int CurrentSegments =>
        Mathf.FloorToInt(currentMana);
}
