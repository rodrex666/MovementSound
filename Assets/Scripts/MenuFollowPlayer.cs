using UnityEngine;

public class MenuFollowPlayer : MonoBehaviour
{
    [Header("Player / Camera")]
    public Transform playerCamera;   // Assign Main Camera

    [Header("Menu Positioning")]
    public float distanceFromPlayer = 1.2f;
    public float heightOffset = 0.0f;
    public float followSpeed = 8f;    // smoothing

    void LateUpdate()
    {
        if (playerCamera == null) return;

        // Target position in front of the player's view
        Vector3 targetPos = playerCamera.position +
                            (playerCamera.forward * distanceFromPlayer);

        // Optional: offset vertically
        targetPos.y += heightOffset;

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

        // Billboard to face the player
        Vector3 lookDir = transform.position - playerCamera.position;
        lookDir.y = 0; // keeps menu upright / not tilting
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}