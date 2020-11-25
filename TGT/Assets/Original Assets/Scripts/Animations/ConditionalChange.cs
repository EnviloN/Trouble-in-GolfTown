using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConditionalChange : MonoBehaviour
{
    public Transform NPC;
    public Transform target;
    public int targetIndex;
    public float maxDistance;
    public AnimWithNav nav;
    public Animator animator;

    public string[] isIdlingVarsName;
    public string isMovingVarsName;
    public string[] isSitingVarsName;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, NPC.position);
        if (dist < maxDistance)
        {
            foreach (string v in isIdlingVarsName)
                animator.SetBool(v, false);
            foreach (string v in isSitingVarsName)
                animator.SetBool(v, false);
            animator.SetBool(isMovingVarsName, true);
            nav.changeDesti(target, targetIndex);
        }
    }
}
