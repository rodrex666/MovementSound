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
    public void playSongs()
    {
        foreach (GameObject soundEmitter in _soundEmitters)
        {
            soundEmitter.GetComponent<FMODPlaywithParameters>().startSong();
        }
    }
    public void stopSongs() {
        foreach (GameObject soundEmitter in _soundEmitters)
        {
            soundEmitter.GetComponent<FMODPlaywithParameters>().stopsSong();
        }
    }
    public void pauseSongs()
    {
        foreach (GameObject soundEmitter in _soundEmitters)
        {
            soundEmitter.GetComponent<FMODPlaywithParameters>().pauseSong();
        }
    }
    public void continueSongs()
    {
        foreach (GameObject soundEmitter in _soundEmitters)
        {
            soundEmitter.GetComponent<FMODPlaywithParameters>().continueSong();
        }
    }

}
