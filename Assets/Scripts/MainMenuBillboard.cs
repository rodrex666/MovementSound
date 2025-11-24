using UnityEngine;

public class MainMenuBillboardLagged : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;

    [Header("Position Settings")]
    public float distanceFromPlayer = 1.35f;
    public float heightOffset = 0f;
    public float followLag = 0.08f;   // 0 = instant, 0.1 = light lag, 0.2 = strong lag
    public AnimationCurve easeCurve;

    [Header("Rotation Settings")]
    public float rotationSmooth = 12f;

    private Vector3 smoothedPosition;
    private float smoothedHeight;

    void Start()
    {
        if (playerCamera != null)
        {
            smoothedPosition = transform.position;
            smoothedHeight = playerCamera.position.y + heightOffset;
        }

        // Default easing curve if none set
        if (easeCurve == null || easeCurve.length == 0)
        {
            easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        }
    }

    void LateUpdate()
    {
        if (playerCamera == null) return;

        // -------------------------------------------------------
        // TARGET POSITION (ideal spot without smoothing)
        // -------------------------------------------------------
        Vector3 forward = playerCamera.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 targetPos = playerCamera.position + forward * distanceFromPlayer;

        // vertical follow (smooth)
        float targetHeight = playerCamera.position.y + heightOffset;
        smoothedHeight = Mathf.Lerp(smoothedHeight, targetHeight, Time.deltaTime * 3f);
        targetPos.y = smoothedHeight;

        // -------------------------------------------------------
        // LAG & EASING
        // -------------------------------------------------------
        // Convert followLag into easing-speed
        float t = easeCurve.Evaluate(Time.deltaTime / followLag);

        smoothedPosition = Vector3.Lerp(smoothedPosition, targetPos, t);

        transform.position = smoothedPosition;

        // -------------------------------------------------------
        // ROTATION (billboard with smoothing)
        // -------------------------------------------------------
        Vector3 lookDir = transform.position - playerCamera.position;
        lookDir.y = 0;

        Quaternion targetRot = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSmooth);
    }
}
