using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetLevel(float sliderValue) {
        sliderValue = sliderValue * 10;
        float volume = sliderValue; // / 10.0f;

        //-30 to 10
        volume = -40.0f + volume;
        mixer.SetFloat("MasterVolume", volume);

        //-80
        if (sliderValue < 1) {
            mixer.SetFloat("MasterVolume", -80.0f);
        }
    }
}
