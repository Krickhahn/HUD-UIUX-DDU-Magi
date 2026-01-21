using UnityEngine;
using UnityEngine.UI;

public class MissionTabToggle : MonoBehaviour
{
    [Header("References")]
    public RectTransform missionTab;
    public RectTransform arrowRect;
    public Button arrowButton;
    public Image arrowImage;

    [Header("Arrow Sprites")]
    public Sprite arrowDown;
    public Sprite arrowUp;

    [Header("MissionTab Positions")]
    public Vector2 closedPosition;   // Før klik (hjørne synligt)
    public Vector2 openPosition;     // Efter klik (helt åben)

    [Header("Arrow Fixed Position")]
    public Vector2 arrowFixedPosition; // Sikrer pilen altid er samme sted

    [Header("Animation")]
    public float slideSpeed = 10f;

    private bool isOpen = false;

    void Start()
    {
        // Sæt startpositioner
        missionTab.anchoredPosition = closedPosition;
        arrowRect.anchoredPosition = arrowFixedPosition;

        arrowImage.sprite = arrowDown;

        arrowButton.onClick.AddListener(ToggleTab);
    }

    void ToggleTab()
    {
        isOpen = !isOpen;
        arrowImage.sprite = isOpen ? arrowUp : arrowDown;
    }

    void Update()
    {
        // Flyt mission tab
        Vector2 target = isOpen ? openPosition : closedPosition;
        missionTab.anchoredPosition = Vector2.Lerp(
            missionTab.anchoredPosition,
            target,
            Time.deltaTime * slideSpeed
        );

        // HOLD pilen fast (meget vigtigt)
        arrowRect.anchoredPosition = arrowFixedPosition;
    }
}
