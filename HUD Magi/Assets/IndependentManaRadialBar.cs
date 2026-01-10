using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class IndependentManaRadialBar : MonoBehaviour
{
    public WeaponManaManager manaManager;
    public WeaponType weaponType;
    public int maxManaSegments = 10;

    [Header("UI")]
    public Image manaImage;

    [Header("Fill range")]
    [Range(0f, 1f)]
    public float minFillAmount = 0.0f;

    [Range(0f, 1f)]
    public float maxFillAmount = 0.33f;

    [Header("Regen")]
    public float regenSpeed = 0.1f;

    float currentFill;

    void Start()
    {
        currentFill = maxFillAmount;
        UpdateUI();
    }


    void Update()
    {
        SyncRegenSpeedWithManager();  // <-- nyt
        Regenerate();
        UpdateUI();
    }


    void Regenerate()
    {
        currentFill += regenSpeed * Time.deltaTime;
        currentFill = Mathf.Clamp(currentFill, minFillAmount, maxFillAmount);
    }

    void UpdateUI()
    {
        manaImage.fillAmount = currentFill;
    }

    // 🔑 GAMEPLAY KALDER DENNE
    public void OnManaUsed(float fillAmountLost)
    {
        currentFill -= fillAmountLost;
        currentFill = Mathf.Max(currentFill, minFillAmount);
    }
    void OnEnable()
    {
        manaManager.OnManaSpent += HandleManaSpent;
    }

    void SyncRegenSpeedWithManager()
    {
        if (manaManager == null) return;

        // Hvor meget fill svarer 1 mana til?
        float fillPerSegment = (maxFillAmount - minFillAmount) / Mathf.Max(1, maxManaSegments);

        // Hent den faktiske regenRate (mana/sek) fra manageren for dette våben
        float regenRate = manaManager.GetRegenRate(weaponType);

        // Konverter til fill/sek for radialbaren
        regenSpeed = regenRate * fillPerSegment;
    }



    void OnDisable()
    {
        manaManager.OnManaSpent -= HandleManaSpent;
    }
    void HandleManaSpent(WeaponType spentWeapon, int amount)
    {
        if (spentWeapon != weaponType)
            return;

        float fillPerSegment =
            (maxFillAmount - minFillAmount) / maxManaSegments;

        float lostFill = amount * fillPerSegment;

        OnManaUsed(lostFill);
    }



}