using UnityEngine;

public class spherePositioning : MonoBehaviour
{
    public GameObject mySphere;
    public GameObject myBlaster;
    //public GameObject myFluffSphere;

    void Start()
    {
        mySphere.transform.parent = this.transform.parent;
        mySphere.transform.localPosition = new Vector3(0, 0, 0);
        myBlaster.transform.parent = this.transform.parent;
        myBlaster.transform.localPosition = new Vector3(0, 0, 0);
        //myFluffSphere.transform.parent = this.transform.parent;
        //myFluffSphere.transform.localPosition = new Vector3(0, 0, 0);
    }
}
