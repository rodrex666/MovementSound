using UnityEngine;
using Oculus.Interaction;

public class VinylFloating_Grabbable : MonoBehaviour
{
    [Header("Floating Settings")]
    public Transform homePoint;
    public float floatSpeed = 2f;
    public float floatHeight = 0.05f;
    public float returnSpeed = 3f;

    public bool isSnappedToPlatter = false;

    private Grabbable grabbable;

    public Transform originalParent;

    void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (grabbable == null || homePoint == null)
            return;

        bool isGrabbed = grabbable.SelectingPointsCount > 0;

        // If grabbed, release snap state
        if (isGrabbed)
        {
            isSnappedToPlatter = false;

            // If this vinyl was the snapped one, free the platter
            if (VinylSnapToPlatter.currentSnappedVinyl == this)
            {
                VinylSnapToPlatter.currentSnappedVinyl = null;

                // Notify the turntable that the vinyl was removed
                VinylSnapToPlatter.NotifyVinylRemoved();

                // Unparent from platter so it doesn't inherit rotation
                if (originalParent != null)
                    transform.SetParent(originalParent, true);

                // Stop platter spin
                Transform platter = homePoint.parent;
                PlatterSpin spin = platter.GetComponentInChildren<PlatterSpin>();
                if (spin != null)
                    spin.isSpinning = false;
            }

            return;
        }

        // If snapped, do not float
        if (isSnappedToPlatter)
            return;

        // Floating bob motion
        float bob = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        Vector3 targetPos = homePoint.position + new Vector3(0, bob, 0);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * returnSpeed
        );
    }
}
