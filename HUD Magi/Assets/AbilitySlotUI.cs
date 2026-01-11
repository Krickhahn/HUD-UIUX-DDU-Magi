using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotUI : MonoBehaviour
{
    public Image selectionRing;

    public void SetSelected(bool selected)
    {
        selectionRing.enabled = selected;
    }
}
