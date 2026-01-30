using TMPro;
using UnityEngine;

public class AbilityManaCostUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI costText;

    public void SetCost(int cost)
    {
        if (costText == null) return;
        costText.text = cost.ToString();
    }

    public void Clear()
    {
        if (costText == null) return;
        costText.text = "";
    }
}
