using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Horse : MonoBehaviour
{

    NavMeshAgent _agent;
    Animator _animator;
    AudioSource _audio_source;


    private GameObject Player;

    public float safeDistance = 10.0f;

    public AudioClip walkAudio;

    private AudioClip defaultAudio;

    private float waitTime = 15.0f;

    private float timer = 0;

    List<string> animations = new List <string> { "Eating", "Idle", "Walking" };


    Vector3 selectRandomDestination() {
        float maxWalkDistance = 10f;
        Vector3 direction = _agent.transform.position  + Random.insideUnitSphere * maxWalkDistance;

        NavMeshHit hit;
        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(direction, out hit, maxWalkDistance, -1);
  
        return hit.position;
    }


    void ChangeAnimationState() {
        int animation_index = Random.Range(0, 3);
        
        if (animations[animation_index] == "Walking")
        {
            //select random destination
            _agent.enabled = true;
            _agent.speed = 1.5f;
            _agent.acceleration = 2.0f;
            _agent.SetDestination(selectRandomDestination());
            _animator.Play("Run_inPlace");
            _audio_source.clip = walkAudio;
            _audio_source.loop = true;
            _audio_source.volume = 1.0f;
            _audio_source.Play();
        }
        else
        {
            _agent.enabled = false;
            _animator.Play(animations[animation_index]);
            _agent.velocity = Vector3.zero;
            _audio_source.clip = defaultAudio;
            _audio_source.loop = false;
            _audio_source.volume = 0.9f;
            //_audio_source.Play();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
        _audio_source = gameObject.GetComponent<AudioSource>();
        defaultAudio = _audio_source.clip;

        if (!_agent.isOnNavMesh)
        {
            Vector3 warpPosition = transform.position;
            _agent.Warp(warpPosition);
            _agent.enabled = false;
            _agent.enabled = true;
        }

        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        float distance = Vector3.Distance(_agent.transform.position, Player.transform.position);


        if (distance <= safeDistance) {
            _agent.enabled = true;
            Vector3 shift = this.transform.position - Player.transform.position;
            _agent.speed = 8.0f;
            _agent.acceleration = 15.0f;
            _agent.SetDestination(_agent.transform.position + shift);
            _animator.Play("Run_inPlace");
            timer = 0;
        }
        else if(_agent.enabled && _agent.remainingDistance < 0.5){ //is not running away, but is at destination
            _agent.SetDestination(_agent.transform.position);
            _agent.velocity = Vector3.zero;
            ChangeAnimationState();
            timer = 0;
        }
        if (timer > waitTime) {
            ChangeAnimationState();
            timer = 0;
        }

    }
}
