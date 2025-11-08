using UnityEngine;

public class spherePositioning : MonoBehaviour
{
    public GameObject mySphere;

    void Start()
    {
        mySphere.transform.parent = this.transform.parent;
        mySphere.transform.localPosition = new Vector3(0, 0, 0);
    }
}
