using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWheelUI : MonoBehaviour
{
    [Header("Ability slots per weapon")]
    public AbilitySlotUI[] magicSlots;
    public AbilitySlotUI[] gunSlots;
    public AbilitySlotUI[] swordSlots;

    WeaponType currentWeapon;
    int currentAbilityIndex;

    public void SetWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;
        UpdateVisuals();
    }

    public void SetSelectedAbility(int index)
    {
        currentAbilityIndex = index;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        ClearAll();

        AbilitySlotUI[] activeSlots = GetSlotsForWeapon(currentWeapon);
        if (activeSlots == null) return;

        if (currentAbilityIndex >= 0 && currentAbilityIndex < activeSlots.Length)
            activeSlots[currentAbilityIndex].SetSelected(true);
    }

    void ClearAll()
    {
        foreach (var slot in magicSlots)
            slot.SetSelected(false);

        foreach (var slot in gunSlots)
            slot.SetSelected(false);

        foreach (var slot in swordSlots)
            slot.SetSelected(false);
    }

    AbilitySlotUI[] GetSlotsForWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Magic: return magicSlots;
            case WeaponType.Gun: return gunSlots;
            case WeaponType.Sword: return swordSlots;
        }
        return null;
    }
}
