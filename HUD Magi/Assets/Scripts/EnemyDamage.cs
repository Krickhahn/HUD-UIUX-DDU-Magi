using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageOnTouch : MonoBehaviour
{
    public float damage = 20f;
    public float hitCooldown = 0.5f;

    float nextHitTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        TryDamage(other);
    }

    void OnTriggerStay(Collider other)
    {
        TryDamage(other);
    }

    void TryDamage(Collider other)
    {
        if (Time.time < nextHitTime) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats == null) return;

        stats.TakeDamage(damage);
        nextHitTime = Time.time + hitCooldown;
    }
}