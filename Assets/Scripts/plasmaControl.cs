using UnityEngine;
using UnityEngine.VFX;

public class plasmaControl : MonoBehaviour
{
    [Header("Plasma VFX")] 
    public VisualEffect plasmaVFX;

    [Header("The Other Sphere")]
    public GameObject sphereOther;

    [Header("VFX Values")]
    public float normalIntense;
    public float sphereDistance;
    private float sphereDistanceNorm;
    public float myY;

    [Header("Velocity Values")]
    [Tooltip("The final, normalized velocity (0 to 1) of the last object that passed through.")]
    [SerializeField]
    public float estimatedNormalizedVelocity;
    public Vector3 previousPosition;
    public float myVelocity;
    public float maxExpectedVelocity;
    public float minParamValue = 0;
    public float maxParamValue = 2;
    public float mySmoothVelocity;

    [Header("Sound Emitter")]
    [SerializeField]
    private FMODPlaywithParameters _FmodParatemers;

    //public GameObject myFluffSphere;
    //public VisualEffect fluffyVFX;

    void Start()
    {
        plasmaVFX = GetComponent<VisualEffect>();
        //fluffyVFX = myFluffSphere.GetComponent<VisualEffect>();
        previousPosition = transform.position;
        mySmoothVelocity = 0f;
    }

    void Update()
    {
        //To keep VFX rotation correct
        transform.rotation = Quaternion.identity;

        //Getting Y position for VFX
        myY = (gameObject.transform.position.y) * 8;

        //Distance between spheres
        Vector3 sphereOtherWorldPos = sphereOther.transform.position;
        Vector3 sphereMeWorldPos = this.transform.position;

        sphereDistance = Vector3.Distance(sphereOtherWorldPos, sphereMeWorldPos);
        sphereDistanceNorm = 5 - (sphereDistance * 5);

        //Velocity calculation
        float velDistance = Vector3.Distance(transform.position, previousPosition);
        myVelocity = velDistance / Time.deltaTime;
        previousPosition = transform.position;

        //Smooth the velocity
        mySmoothVelocity = Mathf.SmoothDamp(mySmoothVelocity, myVelocity, ref mySmoothVelocity, 0.1f);

        //normalising the smooth velocity
        estimatedNormalizedVelocity = Mathf.Clamp(mySmoothVelocity / maxExpectedVelocity, minParamValue, maxParamValue);

        //Update Parameters in Sphere
        _FmodParatemers.updateParameterFMOD(estimatedNormalizedVelocity);

        //Normalising velocity for VFX intensity
        normalIntense = (estimatedNormalizedVelocity * 1.5f);

        //Setting plasma VFX parameters - colour and intensity on velocity, attraction in distance, rate on Y position
        plasmaVFX.SetFloat("colourTime", estimatedNormalizedVelocity);
        plasmaVFX.SetFloat("intensity", normalIntense);
        plasmaVFX.SetFloat("attractionStrength", sphereDistanceNorm);
        plasmaVFX.SetFloat("rate", myY);

        //fluffyVFX.SetFloat("sphereDistance", sphereDistance);
        //fluffyVFX.SetFloat("rate", myY);
        //fluffyVFX.SetFloat("colourTime", estimatedNormalizedVelocity);
    }
}
