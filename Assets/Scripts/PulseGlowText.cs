using UnityEngine;
using TMPro;

public class NeonGlowTextRandomized : MonoBehaviour
{
    public TMP_Text tmpText;

    [Header("Glow Ranges")]
    public float minGlowPower = 0.1f;
    public float maxGlowPower = 0.7f;

    public float minGlowOuter = 0.1f;
    public float maxGlowOuter = 0.3f;

    [Header("Pulse Randomization")]
    public float basePulseSpeed = 1.5f;
    public float pulseSpeedVariance = 0.8f; // random perlin drift

    [Header("Micro Flicker")]
    public float flickerSpeed = 20f;
    public float flickerStrength = 0.05f;

    private Material mat;

    // Per-text random offsets so each glow is unique
    private float noiseOffset;
    private float speedNoiseOffset;

    void Start()
    {
        if (tmpText == null)
            tmpText = GetComponent<TMP_Text>();

        mat = Instantiate(tmpText.fontMaterial);
        tmpText.fontMaterial = mat;

        noiseOffset = Random.Range(0f, 1000f);        // random glow pattern
        speedNoiseOffset = Random.Range(0f, 1000f);   // random speed drift
    }

    void Update()
    {
        float t = Time.time;

        // Random drifting pulse speed (slowly changes over time)
        float dynamicPulseSpeed = basePulseSpeed +
            Mathf.PerlinNoise(t * 0.2f, speedNoiseOffset) * pulseSpeedVariance;

        // Smooth random pulse shape
        float pulse =
            Mathf.PerlinNoise(t * dynamicPulseSpeed, noiseOffset);

        // Convert pulse (0–1) into glow ranges
        float glowPower = Mathf.Lerp(minGlowPower, maxGlowPower, pulse);
        float glowOuter = Mathf.Lerp(minGlowOuter, maxGlowOuter, pulse);

        // Subtle flicker layered on top
        float flicker =
            (Mathf.PerlinNoise(t * flickerSpeed, noiseOffset + 1337f) - 0.5f)
            * flickerStrength;

        mat.SetFloat("_GlowPower", glowPower + flicker);
        mat.SetFloat("_GlowOuter", glowOuter + flicker * 0.5f);
    }
}
