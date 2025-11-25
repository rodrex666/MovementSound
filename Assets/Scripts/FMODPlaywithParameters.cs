using FMODUnity;
using UnityEngine;

public class FMODPlaywithParameters : MonoBehaviour
{
    FMOD.Studio.EventInstance soundInstance;
    
    public FMODUnity.EventReference instrumentEvent;
    [SerializeField]
    private bool _controlParameter = true;
    [SerializeField]
    private string _parameterVolumeName;
    [SerializeField]
    private float _valueVolumeFmod = 2f;
    [SerializeField]
    private string _parameterTypeName;
    [SerializeField]
    private float _valueTypeFmod = 1f;

    private float velocityHandsfinal = 0f;
    private float velocityHandsnew = 0f;

    void Start()
    {
        soundInstance= FMODUnity.RuntimeManager.CreateInstance(instrumentEvent);
        
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundInstance, gameObject, gameObject.GetComponent<Rigidbody>());
        soundInstance.setParameterByName(_parameterTypeName, _valueTypeFmod);
        soundInstance.setParameterByName(_parameterVolumeName, _valueVolumeFmod);
        startSong();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controlParameter)
        { 
            velocityHandsfinal = velocityHandsnew + _valueVolumeFmod*Time.deltaTime;
            
            soundInstance.setParameterByName(_parameterVolumeName, _valueVolumeFmod);
            Debug.Log(gameObject.name + " - " + velocityHandsfinal);
            velocityHandsnew = velocityHandsfinal;
        }
    }
    public void updateParameter(float value)
    {
        _valueVolumeFmod = value;
        
    }
    public void updateParameterTypeFMOD(float value)
    {
        _valueTypeFmod = value;
        soundInstance.setParameterByName(_parameterTypeName, _valueTypeFmod);
    }
    private void OnDestroy()
    {
        soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundInstance.release();
    }
    public void volumeVelocityOn()
    {
        _controlParameter = true;
    }
    public void volumeVelocityOff()
    {
        _controlParameter = false;
    }
    public void startSong()
    {
        soundInstance.start();
    }
    public void stopsSong()
    {
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void pauseSong()
    {
        soundInstance.setPaused(true);
    }
    public void continueSong()
    {
        soundInstance.setPaused(false);
    }
}
