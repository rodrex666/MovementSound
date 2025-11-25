using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEmitterSender : MonoBehaviour
{
    private float _valueVolumeFmod = 2.0f;
    [SerializeField]
    private List<GameObject> _soundEmitters = new List<GameObject>();
    public void updateParameterFMOD(float value)
    {
        _valueVolumeFmod = value;
        changeValuesParameterEmmitters();
    }
    private void changeValuesParameterEmmitters()
    {
        foreach (GameObject soundEmitter in _soundEmitters)
        {
            soundEmitter.GetComponent<FMODPlaywithParameters>().updateParameter(_valueVolumeFmod);
        }
    }
    public void changeSong(float value)
    {
        foreach (GameObject soundEmitter in _soundEmitters)
        {
            soundEmitter.GetComponent<FMODPlaywithParameters>().updateParameterTypeFMOD(value);
        }
    }
}
