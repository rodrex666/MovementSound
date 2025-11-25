using UnityEngine;

public class passthroughColour : MonoBehaviour
{
    public GameObject passthroughHolder;
    public Gradient myColour;
    public OVRPassthroughLayer passComponent;
    public GameObject passthroughOG;
    /// <summary>
    /// If the player is on this section and he moves the hands, the assigned sphere should change the volume
    /// </summary>
    [SerializeField]
    private FMODPlaywithParameters sphereInstrument;

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
            sphereInstrument.volumeVelocityOn();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "headCube")
        {
            passthroughHolder.SetActive(false);
            passthroughOG.SetActive(true);
            sphereInstrument.volumeVelocityOff();
        }
    }
}
