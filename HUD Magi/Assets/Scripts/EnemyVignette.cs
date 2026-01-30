using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnemyVignettePulse : MonoBehaviour
{
    public Volume volume;

    [Header("Distance Settings")]
    public float maxDistance = 15f;

    [Header("Vignette Intensity")]
    public float minIntensity = 0f;
    public float maxIntensity = 0.45f;

    [Header("Pulse Speed")]
    public float minPulseSpeed = 0.5f;
    public float maxPulseSpeed = 4f;

    private Vignette vignette;
    private float pulseTime;

    void Start()
    {
        if (!volume.profile.TryGet(out vignette))
        {
            Debug.LogError("Vignette not found in Volume!");
        }
    }

    void Update()
    {
        if (vignette == null) return;

        float closestDistance = GetClosestEnemyDistance();

        // Hvis ingen enemies i nærheden
        if (closestDistance > maxDistance)
        {
            vignette.intensity.value = 0f;
            return;
        }

        // 0 = langt væk, 1 = helt tæt på
        float t = Mathf.Clamp01(1f - (closestDistance / maxDistance));

        // Pulse speed bliver hurtigere jo tættere man er på
        float pulseSpeed = Mathf.Lerp(minPulseSpeed, maxPulseSpeed, t);

        pulseTime += Time.deltaTime * pulseSpeed;

        // Sinus puls (0 → 1)
        float pulse = (Mathf.Sin(pulseTime) + 1f) * 0.5f;

        float baseIntensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        vignette.intensity.value = baseIntensity * pulse;
    }

    float GetClosestEnemyDistance()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closest = Mathf.Infinity;
        Vector3 playerPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(playerPos, enemy.transform.position);
            if (dist < closest)
                closest = dist;
        }

        return closest;
    }
}
