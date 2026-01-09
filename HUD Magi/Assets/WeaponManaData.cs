using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponManaData
{
    public WeaponType weaponType;

    [Header("Segments")]
    public int manaSegments = 5;
    public float manaPerSegment = 10f;

    [HideInInspector]
    public float currentMana;

    public float MaxMana
    {
        get { return manaSegments * manaPerSegment; }
    }
}
