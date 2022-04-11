using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer mixer;
    public Slider slider;
    void Start()
    {
        if(PlayerPrefs.GetInt("init")==0) PlayerPrefs.SetFloat("audio", 1f);
        slider.value = PlayerPrefs.GetFloat("audio") ;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onChangeSlider(float value){
        PlayerPrefs.SetFloat("audio", value);
        PlayerPrefs.SetInt("init", 1);
        mixer.SetFloat("volume", value);
    }
}
