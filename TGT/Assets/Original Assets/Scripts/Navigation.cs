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
    public bool          mIsAtSittingPos = false;
    private Vector3      mLastPos;
    [Header("Delays/Cooldowns")]
    public float         rollCooldownRangeMin = -1;
    public float         rollCooldownRangeMax = 1;
    public float         rollCooldown = 5;
    [Header("Animation Handling")]
    public Animator      animator;
    private Delay        mAnimationDelay;
    private string       mAnimType = "idling";
    public int           movingProbability = 75;
    public string[]      isIdlingVarsName;
    public string        isMovingVarsName;
    public string[]      isSitingVarsName;

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

    private void StartNav()
    {
        foreach (string v in isIdlingVarsName)
            animator.SetBool(v, false);
        foreach (string v in isSitingVarsName)
            animator.SetBool(v, false);
        animator.SetBool(isMovingVarsName, true);
        // roll new target and set navigation to it
        int index = mIndex;
        while (mIndex == index)
        {
            if (isRandomPicking)
                index = Random.Range(0, mTargets.Length);
            else
                index++;
            index = index % mTargets.Length;
        }
        mIndex = index;
        agent.enabled = true;
        if (mTargets[mIndex].name.StartsWith("[sit]"))
            mIsAtSittingPos = true;
        else
            mIsAtSittingPos = false;
        agent.SetDestination(mTargets[mIndex].transform.position);
    }

    private void TryStopNav()
    {
        float dist = Vector3.Distance(transform.position, mTargets[mIndex].transform.position);
        if (dist < stopingDistance)
        {
            Vector3 pos = agent.destination;
            animator.SetBool(isMovingVarsName, false);
            animator.SetBool(isIdlingVarsName[0], true);
            agent.ResetPath();
            agent.enabled = false;
            if (mIsAtSittingPos)
            {
                mLastPos = agent.destination;
                agent.Warp(mTargets[mIndex].transform.position);
                transform.rotation = mTargets[mIndex].transform.rotation;
            }
        }
    }

    float GetAnimationTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private void AnimationUpdate()
    {
        if (mIsAtSittingPos)
        {
            int nextAnim = Random.Range(0, isSitingVarsName.Length);
            foreach (string v in isSitingVarsName)
                animator.SetBool(v, false);
            foreach (string v in isIdlingVarsName)
                animator.SetBool(v, false);
            animator.SetBool(isMovingVarsName, false);
            animator.SetBool(isSitingVarsName[nextAnim], true);
            mAnimType = "sitting";
        } 
        else
        {
            int nextAnim = Random.Range(0, isIdlingVarsName.Length);
            foreach (string v in isSitingVarsName)
                animator.SetBool(v, false);
            foreach (string v in isIdlingVarsName)
                animator.SetBool(v, false);
            animator.SetBool(isMovingVarsName, false);
            animator.SetBool(isIdlingVarsName[nextAnim], true);
            mAnimType = "idling";
        }
        mAnimationDelay = new Delay(GetAnimationTime() + 0.01f);
    }

    // Start is called before the first frame update
    void Start()
    {
        mIndex = 0;
        mIsStopped = false;
        mTargets = GetTopLevelChildren(targetsGameObject.transform);
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimationDelay != null && mAnimationDelay.isCountingDown())
        {
            mAnimationDelay.update(Time.deltaTime);
        } 
        else
        {
            if (agent.enabled)
            {
                // agent is enabled so we are still navigating
                TryStopNav();
                return;
            }
            Debug.Log("mAnimationDelay not counting down.");
            int nextState = Random.Range(0, 100);
             if ( mAnimType == "idling" &&  nextState > movingProbability)
            {
                // move - navigate to new target

                StartNav();
            }
            else
            {
                if (nextState > movingProbability)
                {
                    mIsAtSittingPos = false;
                }
                // stay at current position and roll for new animation of that type
                AnimationUpdate();
            }
        }
    }
}
