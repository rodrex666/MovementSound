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

    //public GameObject mySphere;
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
        estVelocity1 = vel1.estimatedNormalizedVelocity;
        estVelocity2 = vel2.estimatedNormalizedVelocity;
        estVelocity3 = vel3.estimatedNormalizedVelocity;

        

        transform.rotation = Quaternion.identity;
        aveVel = (estVelocity1 + estVelocity2 + estVelocity3) / 3;
        aveVelNormalColour = Mathf.Clamp(aveVel / 0.5f, 0, 1);
        aveVelNormalIntense = Mathf.Clamp(aveVel / 0.5f, 0.8f, 5f);
        plasmaVFX.SetFloat("colourTime", aveVelNormalColour);
        plasmaVFX.SetFloat("intensity", aveVelNormalIntense);

        myY = (gameObject.transform.position.y) * 8;

        plasmaVFX.SetFloat("rate", myY);
    }
}
