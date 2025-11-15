using UnityEngine;
using UnityEngine.VFX;

public class VelocityEstimator : MonoBehaviour
{
    [Header("Velocity Estimation")]
    [Tooltip("The maximum speed you expect an object to have. This is used to map the calculated velocity to a range. For example, if you set this to 10, an object moving at 10 units/sec or faster will result in a max value.")]
    [SerializeField]
    private float maxExpectedVelocity = 10f;

    [Header("Parameters Sound Value")]
    [Tooltip("Defining the max and min value that accept the FMOD Parameter")]
    [SerializeField]
    private float minParamValue;
    [SerializeField]
    private float maxParamValue;

    [Header("Sound Emitter")]
    [SerializeField]
    private FMODPlaywithParameters _FmodParatemers;

    [Header("Debug Info")]
    [Tooltip("The final, normalized velocity (0 to 1) of the last object that passed through.")]
    [SerializeField] // This makes it visible in the inspector but not publicly settable from other scripts.
    public float estimatedNormalizedVelocity;

    private Vector3 entryPosition;
    private float entryTime;
    private Transform trackedObject; // To keep track of the object currently inside the trigger.


    public GameObject sphere1;
    public GameObject sphere2;
    public VisualEffect plasma1;
    public VisualEffect plasma2;

   


    private void Start()
    {
        plasma1 = sphere1.GetComponent<VisualEffect>();
        plasma2 = sphere2.GetComponent<VisualEffect>();
        Debug.Log("Enter start");
    }
    private void OnTriggerEnter(Collider other)
    {
        // We only start tracking if the trigger is currently empty.
        // This prevents issues if a second object enters before the first one leaves.
        Debug.Log("Enter the trigger: "+other.name);
        if (trackedObject == null && other.CompareTag("Hands"))
        {
            trackedObject = other.transform;
            entryPosition = other.transform.position;
            entryTime = Time.time; // Time.time is the number of seconds since the game started.
            
            Debug.Log(other.name + " entered the detection zone.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        // We only perform the calculation if the object leaving is the one we are currently tracking.
        if (other.transform == trackedObject)
        {
            // --- CALCULATION ---

            // 1. Calculate the distance traveled inside the trigger.
            float distance = Vector3.Distance(entryPosition, other.transform.position);

            // 2. Calculate the time elapsed.
            float timeElapsed = Time.time - entryTime;

            // Avoid division by zero if the object enters and exits on the same frame.
            if (timeElapsed <= 0)
            {
                estimatedNormalizedVelocity = 0;
                trackedObject = null; // Reset for the next object.
                return;
            }

            // 3. Calculate the raw velocity (distance / time).
            float rawVelocity = distance / timeElapsed;

            // 4. Normalize the velocity to a 0-1 range.
            // Mathf.Clamp01 ensures the value is never less than 0 or greater than 1.
           // estimatedNormalizedVelocity = Mathf.Clamp01(rawVelocity / maxExpectedVelocity);
            estimatedNormalizedVelocity = Mathf.Clamp(rawVelocity/maxExpectedVelocity,minParamValue,maxParamValue);

            //Update Parameters in Sphere
            _FmodParatemers.updateParameterFMOD(estimatedNormalizedVelocity);

            // --- DEBUG LOG ---
            Debug.Log(trackedObject.name + " exited. Raw velocity: " + rawVelocity.ToString("F2") + " u/s. Normalized velocity: " + estimatedNormalizedVelocity.ToString("F2"));

            // --- RESET ---
            // Finally, reset the trackedObject so the trigger is ready for the next object.
            trackedObject = null;
        }
        }

    public float GetLastEstimatedVelocity()
    {
        return estimatedNormalizedVelocity;

    }
}
