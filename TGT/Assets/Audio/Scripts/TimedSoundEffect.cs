using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSoundEffect : MonoBehaviour
{

    public float delay;

    private AudioSource audioSource;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > delay) {
            audioSource.Play();
            timer = 0f;
        }
    }
}
