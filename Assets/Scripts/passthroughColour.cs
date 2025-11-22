using UnityEngine;

public class passthroughColour : MonoBehaviour
{
    public GameObject passthroughHolder;
    public Gradient myColour;
    public OVRPassthroughLayer passComponent;
    public GameObject passthroughOG;

    void Start()
    {
        passComponent = passthroughHolder.GetComponent<OVRPassthroughLayer>();
        passthroughHolder.SetActive(false);
        passthroughOG.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "headCube")
        {
            passthroughOG.SetActive(false);
            passthroughHolder.SetActive(true);
            passComponent.colorMapEditorGradient = myColour;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "headCube")
        {
            passthroughHolder.SetActive(false);
            passthroughOG.SetActive(true);
        }
    }
}
