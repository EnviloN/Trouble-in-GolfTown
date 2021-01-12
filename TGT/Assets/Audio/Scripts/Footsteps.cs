using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public CheckIfGrounded checkIfGrounded;

    public AudioSource audioSource;

    public AudioClip[] dirtClips;
    public AudioClip[] woodClips;

    AudioClip previousClip;

    Rigidbody character;
    float currentSpeed;
    private bool walking;
    private float distanceCovered;
    public float modifier = 0.5f;
    public float StepFrequency = 2.0f;
    AudioClip selection;

    //float airTime;


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = character.velocity.magnitude;
        walking = CheckIfWalking();

        if (walking) {
            distanceCovered += currentSpeed * Time.deltaTime * modifier;
            if (distanceCovered > StepFrequency) {
                TriggerNextClip();
                distanceCovered = 0;
            }
        }
    }

    bool CheckIfWalking() {
        if (currentSpeed > 0 && checkIfGrounded.isGrounded)
        {
            return true;
        }
        else return false;
    }

    AudioClip GetClipFromArray(AudioClip[] clipArray) {
        int attempts = 3;
        AudioClip selectedClip = clipArray[Random.Range(0, clipArray.Length - 1)];

        while (selectedClip == previousClip && attempts > 0) {
            selectedClip = clipArray[Random.Range(0, clipArray.Length - 1)];
            attempts--;
        }

        previousClip = selectedClip;
        return selectedClip;

    }

    void TriggerNextClip() {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = Random.Range(0.8f, 1.0f);

        if (checkIfGrounded.isOnStructure)
        {
            selection = GetClipFromArray(woodClips);
            audioSource.PlayOneShot(selection, 1);
        }
        else {
            selection = GetClipFromArray(dirtClips);
            audioSource.PlayOneShot(selection, 1);
        }

    }
}
