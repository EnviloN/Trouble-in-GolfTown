using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCondition : MonoBehaviour
{
    public AnimWithNav  navAnim;
    public Transform    target;
    public float        treshold = 2;
    public string       anim = "IsArguing";
    public bool         isTriggered = false;
    public bool         goToNextIfTriggered = true;
    public Transform    point;
    public float        pointDistance = 5;

    private Delay       mCooldown;
    private Delay       mTriggerCooldown;
    public float        cooldown = 10;
    public float        runCooldown = 1800f;

    // Start is called before the first frame update
    void Start()
    {
        mCooldown = new Delay(cooldown);
        mCooldown.update(cooldown);
        mTriggerCooldown = new Delay(runCooldown);
        mTriggerCooldown.update(runCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        if (!mTriggerCooldown.isCountingDown())
        {
            float dist = Vector3.Distance(target.position, transform.position);
            float dist2 = Vector3.Distance(transform.position, point.position);
            if (((dist < treshold && !isTriggered) || (dist < treshold + 1 && isTriggered)) && dist2 < pointDistance)
            {
                if (!mCooldown.isCountingDown())
                {
                    if (isTriggered)
                    {
                        mTriggerCooldown.reset();
                        navAnim.ResetNavigation(goToNextIfTriggered, anim);
                        isTriggered = false;
                    } 
                    else
                    {
                        mCooldown.reset();
                        navAnim.StopNavigation(anim, point.position);
                        Vector3 x = new Vector3(target.position.x, transform.position.y, target.position.z);
                        transform.rotation = Quaternion.LookRotation(x - transform.position, Vector3.up);
                        isTriggered = true;
                    }
                } else
                {
                    mCooldown.update(Time.deltaTime);
                }
            }
        } 
        else
        {
            mTriggerCooldown.update(Time.deltaTime);
        }
    }
}
