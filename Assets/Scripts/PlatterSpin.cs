using UnityEngine;

public class PlatterSpin : MonoBehaviour
{
    [Header("Spin Settings")]
    public float spinSpeed = -120f; // degrees per second
    public bool isSpinning = false;

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime, Space.Self);
        }
    }
}
