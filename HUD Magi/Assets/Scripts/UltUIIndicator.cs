using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltUIIndicator : MonoBehaviour
{
    [SerializeField] Image cooldownOverlay;
    [SerializeField] Image readyGlow;

    public void UpdateState(bool manaFull, float cooldownNormalized)
    {
        // Cooldown overlay
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = cooldownNormalized;
            cooldownOverlay.enabled = cooldownNormalized > 0f;
        }

        // Glow vises KUN når ult er brugbar
        if (readyGlow != null)
        {
            bool ultReady = manaFull && cooldownNormalized <= 0f;
            readyGlow.enabled = ultReady;
        }
    }
}
