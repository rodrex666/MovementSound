using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class footBlastCollision : MonoBehaviour
{
    public VisualEffect footVFX;
    public float currentParticleCount;
    public float newParticleCount;
    public bool hitCollider;

    void Start()
    {
        footVFX = GetComponent<VisualEffect>();
        hitCollider = false;
    }

    void Update()
    {
        if (hitCollider)
        {
            currentParticleCount = footVFX.GetFloat("particleCount");
            if (currentParticleCount > 0.1f)
            {
                newParticleCount = currentParticleCount - 0.2f;
                footVFX.SetFloat("particleCount", newParticleCount);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        footVFX.SetFloat("particleCount", 80);
        hitCollider = true;
    }
}
