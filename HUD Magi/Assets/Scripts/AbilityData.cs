using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    [Header("Cooldown")]
    public float cooldownTime = 3f;

    public string abilityName;
    public int manaCost;

}
