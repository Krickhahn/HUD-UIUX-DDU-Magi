using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltUIIndicator : MonoBehaviour
{
    public Image ultReadyImage;
    public Image cooldownOverlay;

    public void SetReady(bool ready)
    {
        ultReadyImage.enabled = ready;
    }
    public void SetCooldown(float normalized)
    {
        cooldownOverlay.fillAmount = normalized;
        cooldownOverlay.enabled = normalized > 0f;
    }
}
