using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimWithNav : MonoBehaviour
{
    [Header("Navigation")]
    public NavMeshAgent agent;
    public GameObject targetsGameObject;
    public float stopingDistance = 1.5f;
    public bool isRandomPicking = true;
    private GameObject[] mTargets;
    private bool mIsStopped = false;
    private int mIndex = 0;
    public bool mIsAtSittingPos = false;
    private Vector3 mLastPos;

    [Header("Delays/Cooldowns")]
    public float rollCooldownRangeMin = -1;
    public float rollCooldownRangeMax = 1;
    public float rollCooldown = 5;
    public float sittingCooldown = 0;

    [Header("Animation Handling")]
    public Animator animator;
    private Delay mAnimationDelay;
    private string mAnimType = "idling";
    public int movingProbability = 75;
    public string[] isIdlingVarsName;
    public string isMovingVarsName;
    public string[] isSitingVarsName;

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
    public bool StopNavigation(string anim, Vector3 tp)
    {
        mIsStopped = true;
        if (mAnimType == "sitting")
        {
            agent.enabled = true;
            agent.Warp(tp);
        }
        agent.enabled = false;
        foreach (string v in isIdlingVarsName)
            animator.SetBool(v, false);
        foreach (string v in isSitingVarsName)
            animator.SetBool(v, false);
        animator.SetBool(isMovingVarsName, false);
        animator.SetBool(anim, true);
        return true;
    }

    /// Resumes navigation stopped by StopNavigation
    /// <returns> True if navigation was stopped by StopNavigation, false otherwise.</returns>
    public bool ResetNavigation(bool pickNew, string anim)
    {
        mIsStopped = false;
        agent.enabled = true;
        foreach (string v in isIdlingVarsName)
            animator.SetBool(v, false);
        foreach (string v in isSitingVarsName)
            animator.SetBool(v, false);
        animator.SetBool(isMovingVarsName, true);
        animator.SetBool(anim, false);
        if (pickNew)
        {
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
            if (mTargets[mIndex].name.StartsWith("[sit]"))
                mIsAtSittingPos = true;
            else
                mIsAtSittingPos = false;
            agent.SetDestination(mTargets[mIndex].transform.position);
        } 
        else
        {
            agent.SetDestination(mTargets[mIndex].transform.position);
        }
        return true;
    }

    public void changeDesti(Transform desti, int index)
    {
        if (desti.gameObject.name.StartsWith("[sit]"))
            mIsAtSittingPos = true;
        else
            mIsAtSittingPos = false;
        agent.SetDestination(transform.position);
        mIndex = index;
    }

    private void StartNav()
    {
        agent.enabled = true;
        if (mAnimType == "sitting")
        {
            agent.Warp(mLastPos);
        }
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
        if (mTargets[mIndex].name.StartsWith("[sit]"))
            mIsAtSittingPos = true;
        else
            mIsAtSittingPos = false;
        agent.SetDestination(mTargets[mIndex].transform.position);
    }

    private bool TryStopNav()
    {
        float dist = Vector3.Distance(transform.position, mTargets[mIndex].transform.position);
        if (dist < stopingDistance)
        {
            float sc = 0;
            Vector3 pos = agent.destination;
            animator.SetBool(isMovingVarsName, false);
            animator.SetBool(isIdlingVarsName[0], true);
            agent.ResetPath();
            if (mIsAtSittingPos)
            {
                mLastPos = agent.destination;
                agent.Warp(mTargets[mIndex].GetComponentsInChildren<Transform>()[1].position);
                transform.rotation = mTargets[mIndex].GetComponentsInChildren<Transform>()[1].rotation;
                sc = sittingCooldown;
            }
            agent.enabled = false;
            mAnimationDelay = new Delay(rollCooldown + sc + Random.Range(rollCooldownRangeMin, rollCooldownRangeMax));
            return true;
        }
        return false;
    }

    float GetAnimationTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private void AnimationUpdate()
    {
        if (mIsAtSittingPos)
        {
            int nextAnim = Random.Range(0, isSitingVarsName.Length) % isSitingVarsName.Length;
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
            int nextAnim = Random.Range(0, isIdlingVarsName.Length) % isIdlingVarsName.Length;
            foreach (string v in isSitingVarsName)
                animator.SetBool(v, false);
            foreach (string v in isIdlingVarsName)
                animator.SetBool(v, false);
            animator.SetBool(isMovingVarsName, false);
            animator.SetBool(isIdlingVarsName[nextAnim], true);
            mAnimType = "idling";
        }
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
        if (!mIsStopped)
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
                    if (TryStopNav())
                        AnimationUpdate();
                    return;
                }
                int nextState = Random.Range(0, 100);
                if (nextState > movingProbability)
                {
                    // move - navigate to new target
                    StartNav();
                }
            }
        }
    }
}
