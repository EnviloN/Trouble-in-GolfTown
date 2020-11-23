using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    [Header("Navigation")]
    public NavMeshAgent  agent;
    public GameObject    targetsGameObject;
    public float         stopingDistance = 1.5f;
    public bool          isRandomPicking = true;
    private GameObject[] mTargets;
    private Vector3      mLastDestination;
    private bool         mIsStopped = false;
    private int          mIndex = 0;
    [Header("Delays/Cooldowns")]
    public float         rollCooldownRangeMin = -1;
    public float         rollCooldownRangeMax = 1;
    public float         rollCooldown = 5;
    private Delay        mTargetRollCooldown;
    [Header("Animation Handling")]
    public Animator      animator;
    public string        isIdlingVarName;

    private static GameObject[] GetTopLevelChildren(Transform parent)
    {
        GameObject[] children = new GameObject[parent.childCount];
        for (int ID = 0; ID < parent.childCount; ID++)
        {
            children[ID] = parent.transform.GetChild(ID).gameObject;
        }
        return children;
    }

    // Checks if agent is navigating or not. (Can be used to determine if pawn is moving or just idling)
    public bool IsNavigating()
    {
        return agent.enabled;
    }

    /// Stops current navigation process
    /// <returns> True if agent was navigating. If agent was idling returns false.</returns>
    public bool StopNavigation()
    {
        if (agent.enabled == true)
        {
            mIsStopped = true;
            agent.enabled = false;
            mLastDestination = agent.destination;
            return true;
        }
        return false;
    }

    /// Resumes navigation stopped by StopNavigation
    /// <returns> True if navigation was stopped by StopNavigation, false otherwise.</returns>
    public bool StartNavigation()
    {
        if (mIsStopped == true)
        {
            agent.enabled = true;
            agent.SetDestination(mLastDestination);
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        mIndex = 0;
        mIsStopped = false;
        mTargetRollCooldown = new Delay(rollCooldown);
        mTargets = GetTopLevelChildren(targetsGameObject.transform);
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mTargetRollCooldown.isCountingDown())
        {
            if (!agent.enabled)
            {
                // roll new target and set navigation to it
                if (isRandomPicking)
                    mIndex = Random.Range(0, mTargets.Length);
                else
                    mIndex++;
                mIndex = mIndex % mTargets.Length;
                agent.enabled = true;
                agent.SetDestination(mTargets[mIndex].transform.position);
                if (animator != null)
                    animator.SetBool(isIdlingVarName, false);
                return;
            }
            float dist = Vector3.Distance(transform.position, agent.destination);
            if (dist < stopingDistance)
            {
                agent.ResetPath();
                agent.enabled = false;
                if (animator != null)
                    animator.SetBool(isIdlingVarName, true);
                mTargetRollCooldown = new Delay(rollCooldown + Random.Range(rollCooldownRangeMin, rollCooldownRangeMax));
            }
        } else
        {
            mTargetRollCooldown.update(Time.deltaTime);
        }
    }
}
