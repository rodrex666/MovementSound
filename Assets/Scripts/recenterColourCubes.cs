using UnityEngine;

public class recenterColourCubes : MonoBehaviour
{
    //public Transform playerCamera;

    void Start()
    {
        // Position directly in front of player
        //Vector3 forward = playerCamera.forward;
        //forward.y = 0; // ignore head tilt
        //forward.Normalize();

        //Vector3 placePos = playerCamera.position + forward;

        //this.transform.position = placePos;

        // Rotate once to face the player
        //Vector3 lookDir = this.transform.position - playerCamera.position;
        //lookDir.y = 0;
        //this.transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
