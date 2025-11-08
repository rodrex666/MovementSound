using UnityEngine;
using UnityEngine.VFX;

public class plasmaControl : MonoBehaviour
{
    //public GameObject attractive;
    //public GameObject attracted;
    public VisualEffect plasmaVFX;
    //public Vector3 attractedPos;

    void Start()
    {
        //attractedPos = attracted.transform.position;
        plasmaVFX = GetComponent<VisualEffect>();
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
