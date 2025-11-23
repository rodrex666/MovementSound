using UnityEngine;
using UnityEngine.VFX;

public class plasmaControl : MonoBehaviour
{
    //public GameObject attractive;
    //public GameObject attracted;
    public VisualEffect plasmaVFX;
    //public Vector3 attractedPos;
    public float aveVel;
    public float estVelocity1;
    public float estVelocity2;
    public float estVelocity3;
    public VelocityEstimator vel1;
    public VelocityEstimator vel2;
    public VelocityEstimator vel3;
    public GameObject velHold1;
    public GameObject velHold2;
    public GameObject velHold3;
    public float aveVelNormalIntense;
    public float aveVelNormalColour;

    public GameObject sphereOther;
    public float sphereDistance;
    private float sphereDistanceNorm;
    public float myY;

    void Start()
    {
        //attractedPos = attracted.transform.position;
        plasmaVFX = GetComponent<VisualEffect>();
        vel1 = velHold1.GetComponent<VelocityEstimator>();
        vel2 = velHold2.GetComponent<VelocityEstimator>();
        vel3 = velHold3.GetComponent<VelocityEstimator>();
    }

    void Update()
    {
        Vector3 sphereOtherWorldPos = sphereOther.transform.position;
        Vector3 sphereMeWorldPos = this.transform.position;

        //Vector3 sphereOtherLocalPos = sphereOther.transform.InverseTransformPoint(sphereOtherWorldPos);
        //Vector3 sphereMeLocalPos = this.transform.InverseTransformPoint(sphereMeWorldPos);

        sphereDistance = Vector3.Distance(sphereOtherWorldPos, sphereMeWorldPos);
        sphereDistanceNorm = 5 - (sphereDistance * 5);

        estVelocity1 = vel1.estimatedNormalizedVelocity;
        estVelocity2 = vel2.estimatedNormalizedVelocity;
        estVelocity3 = vel3.estimatedNormalizedVelocity;



        transform.rotation = Quaternion.identity;
        aveVel = (estVelocity1 + estVelocity2 + estVelocity3) / 3;
        aveVelNormalColour = (aveVel / 2); 
        aveVelNormalIntense = (aveVel * 1.5f);
        plasmaVFX.SetFloat("colourTime", aveVelNormalColour);
        plasmaVFX.SetFloat("intensity", aveVelNormalIntense);

        plasmaVFX.SetFloat("attractionStrength", sphereDistanceNorm);

        myY = (gameObject.transform.position.y) * 8;

        plasmaVFX.SetFloat("rate", myY);
    }
}
