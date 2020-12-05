using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;


    public void SetLevel(float sliderValue) {
        sliderValue = sliderValue * 2;
        float volume = sliderValue / 10.0f;

        if (sliderValue == 0) {
            volume = 0.0001f;
        }

        mixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        

    }
}
