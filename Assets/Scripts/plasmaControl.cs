using UnityEngine;
using UnityEngine.VFX;

public class plasmaControl : MonoBehaviour
{
    public VisualEffect plasmaVFX;
    public float normalIntense;
    public float normalColour;

    public GameObject sphereOther;
    public float sphereDistance;
    private float sphereDistanceNorm;
    public float myY;

    public Vector3 previousPosition;
    public float myVelocity;

    public float maxExpectedVelocity;

    public float minParamValue = 0;
    public float maxParamValue = 2;

    [Header("Sound Emitter")]
    [SerializeField]
    private FMODPlaywithParameters _FmodParatemers;

    [Header("Debug Info")]
    [Tooltip("The final, normalized velocity (0 to 1) of the last object that passed through.")]
    [SerializeField]
    public float estimatedNormalizedVelocity;

    void Start()
    {
        plasmaVFX = GetComponent<VisualEffect>();
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 sphereOtherWorldPos = sphereOther.transform.position;
        Vector3 sphereMeWorldPos = this.transform.position;

        sphereDistance = Vector3.Distance(sphereOtherWorldPos, sphereMeWorldPos);
        sphereDistanceNorm = 5 - (sphereDistance * 5);


        float velDistance = Vector3.Distance(transform.position, previousPosition);
        myVelocity = velDistance / Time.deltaTime;
        previousPosition = transform.position;

        estimatedNormalizedVelocity = Mathf.Clamp(myVelocity / maxExpectedVelocity, minParamValue, maxParamValue);

        //Update Parameters in Sphere
        _FmodParatemers.updateParameterFMOD(estimatedNormalizedVelocity);

        transform.rotation = Quaternion.identity;

        normalColour = (estimatedNormalizedVelocity);
        normalIntense = (estimatedNormalizedVelocity * 1.5f);

        plasmaVFX.SetFloat("colourTime", normalColour);
        plasmaVFX.SetFloat("intensity", normalIntense);

        plasmaVFX.SetFloat("attractionStrength", sphereDistanceNorm);

        myY = (gameObject.transform.position.y) * 8;

        plasmaVFX.SetFloat("rate", myY);
    }
}
