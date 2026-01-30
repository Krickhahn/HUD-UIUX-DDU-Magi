using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialStatBar : MonoBehaviour
{
    [Header("UI")]
    public Image barImage;

    [Header("Arc range (partial circle)")]
    [Range(0f, 1f)]
    public float minFill = 0.53f;

    [Range(0f, 1f)]
    public float maxFill = 0.97f;

    [Header("Stat values")]
    public float maxValue = 100f;
    public float currentValue = 100f;

    [Header("Regen (per second)")]
    public float regenRate = 0f;

    void Start()
    {
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);
        UpdateUI();
    }

    void Update()
    {
        Regenerate();
        UpdateUI();
    }

    void Regenerate()
    {
        if (regenRate <= 0f)
            return;

        currentValue += regenRate * Time.deltaTime;
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);
    }

    /// <summary>
    /// Consume health / stamina / mana
    /// </summary>
    public void Consume(float amount)
    {
        currentValue = Mathf.Clamp(currentValue - amount, 0f, maxValue);
    }

    /// <summary>
    /// Directly set the value (useful when syncing with PlayerStats)
    /// </summary>
    public void SetValue(float value)
    {
        currentValue = Mathf.Clamp(value, 0f, maxValue);
    }

    void UpdateUI()
    {
        float normalized = currentValue / maxValue;
        barImage.fillAmount = Mathf.Lerp(minFill, maxFill, normalized);
    }

    public bool IsEmpty()
    {
        return currentValue <= 0.01f;
    }

    public bool IsFull()
    {
        return Mathf.Approximately(currentValue, maxValue);
    }
}