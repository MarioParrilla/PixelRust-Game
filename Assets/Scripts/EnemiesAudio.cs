using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemiesAudio : MonoBehaviour
{

    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("Volume", PlayerPrefs.GetFloat("audio"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
