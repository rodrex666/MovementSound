using UnityEngine;

public class VinylSnapToPlatter : MonoBehaviour
{
    [Header("Snap Point")]
    public Transform snapPoint;

    [Header("Tonearm Auto-Move")]
    public Transform tonearmPivot;
    public Transform tonearmRestPose;
    public Transform tonearmPlayPose;
    public float tonearmMoveSpeed = 2f;

    [Header("Settings")]
    public float snapSpeed = 10f;
    public float snapRotationSpeed = 10f;

    public static VinylFloating_Grabbable currentSnappedVinyl = null;

    [Header("Vinyl Options and their parameters")]
    public GameObject vinyl1;
    public GameObject vinyl2;
    public GameObject vinyl3;

    public FMODPlaywithParameters fmodVoice;
    public FMODPlaywithParameters fmodBass;
    public FMODPlaywithParameters fmodDrums;
    public FMODPlaywithParameters fmodGuitar;
    public GameObject voice;
    public GameObject bass;
    public GameObject drums;
    public GameObject guitar;

    public string vinylName;
    public float fmodVinylNumber;

    void Start()
    {
        fmodVoice = voice.GetComponent<FMODPlaywithParameters>();
        fmodBass = bass.GetComponent<FMODPlaywithParameters>();
        fmodDrums = drums.GetComponent<FMODPlaywithParameters>();
        fmodGuitar = guitar.GetComponent<FMODPlaywithParameters>();
    }

    private void OnTriggerEnter(Collider other)
    {
        VinylFloating_Grabbable vinyl = other.GetComponent<VinylFloating_Grabbable>();
        if (vinyl == null) return;

        // Block if a vinyl is already snapped
        if (currentSnappedVinyl != null && currentSnappedVinyl != vinyl)
            return;

        StartCoroutine(SnapRoutine(other.transform, vinyl));
    }

    private System.Collections.IEnumerator SnapRoutine(Transform vinylTransform, VinylFloating_Grabbable vinyl)
    {
        Transform originalParent = vinylTransform.parent;
        vinyl.originalParent = originalParent;

        Vector3 startPos = vinylTransform.position;
        Quaternion startRot = vinylTransform.rotation;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;

            vinylTransform.position = Vector3.Lerp(startPos, snapPoint.position, t);
            vinylTransform.rotation = Quaternion.Lerp(startRot, snapPoint.rotation, t * snapRotationSpeed);

            yield return null;
        }

        vinylTransform.position = snapPoint.position;
        vinylTransform.rotation = snapPoint.rotation;

        vinylTransform.SetParent(snapPoint.parent);

        vinyl.isSnappedToPlatter = true;

        //check which vinyl is on
        vinylName = vinyl.name;

        if (vinylName == vinyl1.name)
        {
            fmodVinylNumber = 1;
        }
        else if (vinylName == vinyl2.name)
        {
            fmodVinylNumber = 2;
        }
        else if (vinylName == vinyl3.name)
        {
            fmodVinylNumber = 3;
        }

        //set fmod to play that vinyl
        fmodVoice._valueTypeFmod = fmodVinylNumber;
        fmodBass._valueTypeFmod = fmodVinylNumber;
        fmodDrums._valueTypeFmod = fmodVinylNumber;
        fmodGuitar._valueTypeFmod = fmodVinylNumber;

        //call the function to update fmod
        fmodVoice.updateParameterTypeFMOD(fmodVinylNumber);
        fmodBass.updateParameterFMOD(fmodVinylNumber);
        fmodDrums.updateParameterFMOD(fmodVinylNumber);
        fmodGuitar.updateParameterFMOD(fmodVinylNumber);


        // Start platter spinning
        PlatterSpin platterSpin = snapPoint.parent.GetComponentInChildren<PlatterSpin>();
        if (platterSpin != null)
            platterSpin.isSpinning = true;

        currentSnappedVinyl = vinyl;

        // ⭐ Move tonearm onto the record
        if (tonearmPivot != null && tonearmPlayPose != null)
            StartCoroutine(MoveTonearmRoutine(tonearmPivot, tonearmPlayPose));
    }

    // Handle vinyl removal
    public static void NotifyVinylRemoved()
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.MoveTonearmRoutine(
                instance.tonearmPivot,
                instance.tonearmRestPose
            ));
        }

        currentSnappedVinyl = null;
    }

    // Smooth automatic tonearm swing
    private System.Collections.IEnumerator MoveTonearmRoutine(Transform pivot, Transform targetPose)
    {
        Quaternion startRot = pivot.localRotation;
        Quaternion targetRot = targetPose.localRotation;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * tonearmMoveSpeed;
            pivot.localRotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        pivot.localRotation = targetRot;
    }

    // Singleton instance so static remove call works
    private static VinylSnapToPlatter instance;

    private void Awake()
    {
        instance = this;
    }
}
