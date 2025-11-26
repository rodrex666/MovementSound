using UnityEngine;

public class TurntableGestureToggle : MonoBehaviour
{
    [Header("Hand References")]
    public OVRHand leftHand;
    public OVRHand rightHand;

    [Header("Turntable")]
    public GameObject turntable;

    [Header("Placement Settings")]
    public Transform playerCamera;
    public float distance = 0.5f;

    [Header("Gesture Settings")]
    public float gestureCooldown = 0.8f;
    private float lastGestureTime = 0f;
    private bool isActive = false;

    public GameObject rockOn;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main.transform;

        turntable.SetActive(false);
    }

    void LateUpdate()
    {
        if (leftHand == null || rightHand == null) return;

        if (IsRockGesture(leftHand) || IsRockGesture(rightHand))
            TryToggleTurntable();
    }

    private void TryToggleTurntable()
    {
        if (Time.time - lastGestureTime < gestureCooldown)
            return;

        lastGestureTime = Time.time;

        isActive = !isActive;
        turntable.SetActive(isActive);

        if (isActive)
            PlaceTurntable();
            Destroy(rockOn);
            
    }

    // ---------------------------------------------------------
    // STATIC placement — no follow, no movement, no nausea
    // ---------------------------------------------------------
    private void PlaceTurntable()
    {
        // Position directly in front of player
        Vector3 forward = playerCamera.forward;
        forward.y = 0; // ignore head tilt
        forward.Normalize();

        Vector3 placePos = playerCamera.position + forward * distance;

        turntable.transform.position = placePos;

        // Rotate once to face the player
        Vector3 lookDir = turntable.transform.position - playerCamera.position;
        lookDir.y = 0;
        turntable.transform.rotation = Quaternion.LookRotation(lookDir);
    }

    // ------------------------------
    // Rock & Roll Gesture Detection
    // ------------------------------
    private bool IsRockGesture(OVRHand hand)
    {
        if (!hand.IsTracked) return false;

        bool indexExtended =
            !hand.GetFingerIsPinching(OVRHand.HandFinger.Index) &&
            hand.GetFingerPinchStrength(OVRHand.HandFinger.Index) < 0.25f;

        bool pinkyExtended =
            !hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky) &&
            hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky) < 0.25f;

        bool middleCurled = hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) > 0.45f;
        bool ringCurled = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring) > 0.45f;

        return indexExtended && pinkyExtended && middleCurled && ringCurled;
    }
}
