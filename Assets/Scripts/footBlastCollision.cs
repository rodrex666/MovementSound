using Unity.VisualScripting;
using UnityEngine;

public class footBlastCollision : MonoBehaviour
{
    public GameObject explosionHolder;
    public ParticleSystem explodeParticles;
    public Vector3 clapPosition;

    void Start()
    {
        //assign explosion effect
        explodeParticles = explosionHolder.GetComponent<ParticleSystem>();
    }

    //move explosion effect to collision location, and play explosion effect on collision with blast collider
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "blastCollider")
        {
            clapPosition = transform.position;
            explodeParticles.Stop();
            explosionHolder.transform.position = clapPosition;
            explodeParticles.Play();
        }
    }
}
