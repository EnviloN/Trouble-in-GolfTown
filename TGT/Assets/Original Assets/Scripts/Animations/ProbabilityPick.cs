using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityPick : MonoBehaviour
{
    
    public Animator     animator;
    public string[]     varNames;
    public float[]      probabilities;
    public int          range = 100;

    private Delay       mCooldown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (mCooldown != null && mCooldown.isCountingDown())
        {
            mCooldown.update(Time.deltaTime);
            return;
        }
        int random = Random.Range(0, range);
        foreach (var v in varNames)
            animator.SetBool(v, false);
        bool isSet = false;
        for (int i = 0; i < varNames.Length; i++)
        {
            if (probabilities[i] < random)
            {
                mCooldown = new Delay(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
                animator.SetBool(varNames[i], true);
                isSet = true;
                if (i > 0)
                {
                    animator.SetBool(varNames[i - 1], false);
                }
            }
        }
        if (!isSet)
        {
            mCooldown = new Delay(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        }
    }
}
