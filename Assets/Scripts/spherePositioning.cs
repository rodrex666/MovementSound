using UnityEngine;

public class spherePositioning : MonoBehaviour
{
    public GameObject mySphere;
    public Vector3 myPos;

    void Start()
    {
        mySphere.transform.parent = this.transform.parent;
        mySphere.transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        //myPos = gameObject.transform.position;
        //mySphere.transform.Translate(myPos);
        //mySphere.transform.Rotate(Quaternion.identity);

    }
}
