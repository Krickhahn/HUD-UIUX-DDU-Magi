using UnityEngine;
using UnityEngine.UI;

public class MissionTabToggle : MonoBehaviour
{
    [Header("References")]
    public RectTransform missionTab;
    public Button arrowButton;
    public Image arrowImage;

    [Header("Arrow Sprites")]
    public Sprite arrowDown;
    public Sprite arrowUp;

    [Header("MissionTab Positions")]
    public Vector2 closedPosition;   // Hjørne synligt
    public Vector2 openPosition;     // Fuldt åben

    [Header("Animation")]
    public float slideSpeed = 10f;

    private bool isOpen = false;

    void Start()
    {
        missionTab.anchoredPosition = closedPosition;
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
        Vector2 target = isOpen ? openPosition : closedPosition;

        missionTab.anchoredPosition = Vector2.Lerp(
            missionTab.anchoredPosition,
            target,
            Time.deltaTime * slideSpeed
        );
    }
}
