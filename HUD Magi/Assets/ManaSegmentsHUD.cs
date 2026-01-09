using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSegmentsHUD : MonoBehaviour
{
    public WeaponManaManager manaManager;
    public Image segmentPrefab;
    public Transform container;

    private Image[] segments;

    void Start()
    {
        CreateSegments();
    }

    void Update()
    {
        UpdateSegments();
    }

    void CreateSegments()
    {
        int count = manaManager.GetSegmentCount();
        segments = new Image[count];

        for (int i = 0; i < count; i++)
        {
            Image seg = Instantiate(segmentPrefab, container);
            segments[i] = seg;
        }
    }

    void UpdateSegments()
    {
        WeaponType weapon = manaManager.weaponManager.GetCurrentWeapon();
        WeaponManaData data = null;

        foreach (WeaponManaData w in manaManager.weaponManaData)
        {
            if (w.weaponType == weapon)
                data = w;
        }

        if (data == null) return;

        for (int i = 0; i < segments.Length; i++)
        {
            float segmentValue = (i + 1) * data.manaPerSegment;
            segments[i].enabled = data.currentMana >= segmentValue;
        }
    }
}
