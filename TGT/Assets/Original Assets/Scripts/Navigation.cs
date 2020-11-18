using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    [Header("Navigation")]
    public NavMeshAgent  agent;
    public GameObject    targetsGameObject;
    private GameObject[] mTargets;
    private bool         mIsNavigating = false;
    [Header("Delays/Cooldowns")]
    public float         rollCooldownRangeMin = -1;
    public float         rollCooldownRangeMax = 1;
    public float         rollCooldown = 5;
    private Delay        mTargetRollCooldown;

    private static GameObject[] GetTopLevelChildren(Transform parent)
    {
        GameObject[] children = new GameObject[parent.childCount];
        for (int ID = 0; ID < parent.childCount; ID++)
        {
            children[ID] = parent.transform.GetChild(ID).gameObject;
        }
        return children;
    }

    // Start is called before the first frame update
    void Start()
    {
        mTargetRollCooldown = new Delay(rollCooldown);
        mTargets = GetTopLevelChildren(targetsGameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!mTargetRollCooldown.isCountingDown())
        {
            if (!mIsNavigating)
            {
                // roll new target and set navigation to it
                int index = Random.Range(0, mTargets.Length);
                agent.SetDestination(mTargets[index].transform.position);
                mIsNavigating = true;
                return;
            }
            if (Vector3.Distance(transform.position, agent.destination) < 0.15)
            {
                agent.ResetPath();
                mIsNavigating = false;
                mTargetRollCooldown = new Delay(rollCooldown + Random.Range(rollCooldownRangeMin, rollCooldownRangeMax));
            }
        }
    }
}
