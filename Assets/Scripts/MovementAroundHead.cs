
using UnityEngine;

public class MovementAroundHead : MonoBehaviour
{
    [Header("Orbit Settings")]
    [Tooltip("The camera this object will orbit around.")]
    public Camera mainCamera;

    [Tooltip("The distance to keep from the camera.")]
    public float radius = 5.0f;

    [Tooltip("How fast the object orbits the camera (degrees per second).")]
    public float orbitSpeed = 45.0f;
    void Start()
    {
        // If no camera is assigned in the Inspector, find the main camera automatically.
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Set the initial position to be 'radius' units in front of the camera.
        // This prevents the object from starting at the world origin (0,0,0).
        if (mainCamera != null)
        {
            transform.position = mainCamera.transform.position + mainCamera.transform.forward * radius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the camera exists to avoid errors.
        if (mainCamera == null)
        {
            return;
        }

        // --- 1. MOVEMENT ---
        // This is the core function for movement. It rotates this game object's
        // position around the camera's position.
        // - mainCamera.transform.position: The point to orbit around.
        // - Vector3.up: The axis to rotate around (the vertical Y-axis).
        // - orbitSpeed * Time.deltaTime: The speed of rotation, made smooth and
        //   independent of the frame rate.
        transform.RotateAround(mainCamera.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);


        // --- 2. ROTATION ---
        // This instantly rotates the object to face the camera's position.
        // It's the simplest way to make one object look at another.
        transform.LookAt(mainCamera.transform);
    }
}
