using UnityEngine;

public class FMODPlaywithParameters : MonoBehaviour
{
    FMOD.Studio.EventInstance MainMix;
    private float _valueFmod =0.5f;

    void Start()
    {
        MainMix = FMODUnity.RuntimeManager.CreateInstance("event:/Main_mix");
        MainMix.start();
    }

    // Update is called once per frame
    void Update()
    {
        MainMix.setParameterByName("EnergyLevel",_valueFmod);
    }
    public void updateParameterFMOD(float value)
    {
        _valueFmod = value;
    }
}
