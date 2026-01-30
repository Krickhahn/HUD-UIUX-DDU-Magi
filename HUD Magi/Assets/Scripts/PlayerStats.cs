using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float health = 100f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina = 100f;

    [Header("UI")]
    public RadialStatBar healthBar;
    public RadialStatBar staminaBar;

    [Header("Stamina Settings")]
    public float staminaRegenRate = 20f;
    public float sprintDrainRate = 25f;
 

    void Start()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;

        healthBar.SetValue(health);
        staminaBar.SetValue(stamina);
    }

    void Update()
    {
        RegenerateStamina();
    }

    // -------- HEALTH --------

    public void TakeDamage(float amount)
    {
        health = Mathf.Clamp(health - amount, 0f, maxHealth);
        healthBar.SetValue(health);

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        Debug.Log("Player died - Restarting scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // -------- STAMINA --------

    public bool UseStamina(float amount)
    {
        if (stamina < amount)
            return false;

        stamina -= amount;
        staminaBar.SetValue(stamina);
        return true;
    }

    void RegenerateStamina()
    {
        if (stamina >= maxStamina)
            return;

        stamina += staminaRegenRate * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaBar.SetValue(stamina);
    }
}