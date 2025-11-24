using UnityEngine;

public class VinylSnapToPlatter : MonoBehaviour
{
    [Header("Snap Point")]
    public Transform snapPoint;

    [Header("Settings")]
    public float snapSpeed = 10f;
    public float snapRotationSpeed = 10f;

    public static VinylFloating_Grabbable currentSnappedVinyl = null;

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

            // Move toward snap point
            vinylTransform.position = Vector3.Lerp(startPos, snapPoint.position, t);

            // Rotate upright
            vinylTransform.rotation = Quaternion.Lerp(startRot, snapPoint.rotation, t * snapRotationSpeed);

            yield return null;
        }

        // Final placement
        vinylTransform.position = snapPoint.position;
        vinylTransform.rotation = snapPoint.rotation;

        // Parent to turntable (optional)
        vinylTransform.SetParent(snapPoint.parent);

        // Mark as snapped
        vinyl.isSnappedToPlatter = true;

        // Start platter spinning
        PlatterSpin platterSpin = snapPoint.parent.GetComponentInChildren<PlatterSpin>();
        if (platterSpin != null)
        {
            platterSpin.isSpinning = true;
        }

        currentSnappedVinyl = vinyl;
    }
}
