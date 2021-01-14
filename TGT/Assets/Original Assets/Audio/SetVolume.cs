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

        if (sliderValue == 0) {
            volume = 0.0001f;
        }
        //-40 to 10
        volume = -30.0f + volume; //Mathf.Log10(volume) * 20;
        mixer.SetFloat("MasterVolume", volume);
        //Debug.Log("New volume set at:" + volume);
    }
}
