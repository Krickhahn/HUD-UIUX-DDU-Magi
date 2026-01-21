using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSegmentsHUD : MonoBehaviour
{
    public WeaponManaManager manaManager;
    public Image segmentPrefab;
    public RectTransform container;

    [Header("Layout")]
    public float radius = 60f;
    public float startAngle = -90f;

    [Tooltip("Total vinkel som mana-baren fylder (fx 270)")]
    public float totalAngle = 270f;

    [Tooltip("Mellemrum i grader mellem hvert segment")]
    public float gapAngle = 4f;

    public bool clockwise = true;

    private Image[] segments;
    private int lastMaxMana = -1;

    void Update()
    {
        int maxMana = manaManager.GetMaxMana();

        if (maxMana != lastMaxMana)
        {
            BuildSegments(maxMana);
            lastMaxMana = maxMana;
        }

        UpdateSegments();
    }

    void BuildSegments(int count)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);

        segments = new Image[count];

        // total vinkel der reelt bruges af segmenter
        float totalGap = gapAngle * (count - 1);
        float usableAngle = totalAngle - totalGap;

        float angleStep = usableAngle / count;

        for (int i = 0; i < count; i++)
        {
            Image seg = Instantiate(segmentPrefab, container);
            RectTransform rt = seg.rectTransform;

            float angleOffset =
                i * (angleStep + gapAngle);

            float angle =
                startAngle +
                (clockwise ? -angleOffset : angleOffset);

            Vector2 pos = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            ) * radius;

            rt.anchoredPosition = pos;
            rt.localRotation = Quaternion.Euler(0, 0, angle);

            segments[i] = seg;
        }
    }

    void UpdateSegments()
    {
        int currentMana = manaManager.GetCurrentMana();

        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].enabled = i < currentMana;
        }
    }
}
