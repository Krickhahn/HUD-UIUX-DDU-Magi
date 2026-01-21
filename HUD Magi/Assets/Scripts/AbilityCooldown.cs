using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldown
{
    float cooldownTime;
    float timer;

    public AbilityCooldown(float cooldown)
    {
        cooldownTime = cooldown;
        timer = 0f;
    }

    public void Start()
    {
        timer = cooldownTime;
    }

    public void Update(float deltaTime)
    {
        if (timer > 0f)
            timer -= deltaTime;
    }

    public bool IsReady()
    {
        return timer <= 0f;
    }

    public float Normalized()
    {
        if (cooldownTime <= 0f) return 0f;
        return Mathf.Clamp01(timer / cooldownTime);
    }
}
