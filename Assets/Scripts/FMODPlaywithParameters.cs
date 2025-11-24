using FMODUnity;
using UnityEngine;

public class FMODPlaywithParameters : MonoBehaviour
{
    FMOD.Studio.EventInstance MainMix;
    private float _valueFmod =1f;
    public FMODUnity.EventReference instrumentEvent;
    [SerializeField]
    private bool _controlParameter = true;

    void Start()
    {
        MainMix= FMODUnity.RuntimeManager.CreateInstance(instrumentEvent);
        MainMix.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(MainMix, gameObject, gameObject.GetComponent<Rigidbody>());
    }

    // Update is called once per frame
    void Update()
    {
        if (_controlParameter)
        { MainMix.setParameterByName("MainMixVolume", _valueFmod); }
    }
    public void updateParameterFMOD(float value)
    {
        _valueFmod = value;
    }
    private void OnDestroy()
    {
        MainMix.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MainMix.release();
    }
}
