using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class footBlastCollision : MonoBehaviour
{
    //public VisualEffect footVFX;
    //public float currentParticleCount;
    //public float newParticleCount;

    public GameObject explosionHolder;
    public ParticleSystem explodeParticles;

    public Vector3 clapPosition;

    public bool hitCollider;


    void Start()
    {
        //footVFX = GetComponent<VisualEffect>();
        hitCollider = false;

        explodeParticles = explosionHolder.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //if (hitCollider)
        //{
        //    currentParticleCount = footVFX.GetFloat("particleCount");
        //    if (currentParticleCount > 0.1f)
        //    {
        //        newParticleCount = currentParticleCount - 0.2f;
        //        footVFX.SetFloat("particleCount", newParticleCount);
        //    }
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "blastCollider")
        {
            clapPosition = transform.position;
            explodeParticles.Stop();
            explosionHolder.transform.position = clapPosition;
            explodeParticles.Play();
            hitCollider = true;
        }
    }
}
