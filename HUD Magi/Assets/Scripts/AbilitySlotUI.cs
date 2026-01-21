using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotUI : MonoBehaviour
{
    public Image selectionRing;
    public Image cooldownOverlay;

    public void SetSelected(bool selected)
    {
        selectionRing.enabled = selected;
    }

    public void SetCooldown(float normalized)
    {
        cooldownOverlay.fillAmount = normalized;
        cooldownOverlay.enabled = normalized > 0f;
    }

}
