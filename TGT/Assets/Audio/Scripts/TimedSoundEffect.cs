using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSoundEffect : MonoBehaviour
{

    public float delay;
    public float distance = 0f;
    public bool addRandomDelay = true;

    private AudioSource audioSource;
    private float timer;
    private float randomDelay = 0f;
    private float currentDistance;
    private float clipDuration;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        timer = 0f;
        clipDuration = 0f;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (distance < 0.01f) {
            timer += Time.deltaTime;

            if (timer > (delay + randomDelay) && timer > clipDuration)
            {
                audioSource.Play();
                if(addRandomDelay)
                    randomDelay = Random.Range(-(delay / 3), delay / 3);
                timer = 0f;
                clipDuration = audioSource.clip.length;
            }
        }
        else {
            currentDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (currentDistance < distance)
            {
                //Debug.Log("In range for sound effect.");

                timer += Time.deltaTime;

                if (timer > (delay+randomDelay) && timer > clipDuration)
                {
                    //Debug.Log("Playing horse snort.");
                    audioSource.Play();
                    if (addRandomDelay)
                        randomDelay = Random.Range(-(delay / 3), delay / 3);
                    timer = 0f;
                    clipDuration = audioSource.clip.length;
                }
            }
        }
        
    }
}
