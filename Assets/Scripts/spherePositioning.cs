using UnityEngine;

public class spherePositioning : MonoBehaviour
{
    public GameObject mySphere;
    public GameObject myBlaster;

    void Start()
    {
        mySphere.transform.parent = this.transform.parent;
        mySphere.transform.localPosition = new Vector3(0, 0, 0);
        myBlaster.transform.parent = this.transform.parent;
        myBlaster.transform.localPosition = new Vector3(0, 0, 0);
    }
}
