﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooting : MonoBehaviour
{

    public Inventory    inventory;
    public KeyCode      interactKey;
    public Camera       cam;

    private bool        mInitialized;
    float               mStrength;
    GameObject          mObj;

    public float       maxStrength;
    public float       minStrength;

    // Start is called before the first frame update
    void Start()
    {
        mInitialized = false;
        mStrength = 0f;
    }

    private GameObject raycastToObject()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit);
        if (hit.collider.gameObject.GetComponent<Shootable>())
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        bool haveClubInHand = inventory.havePutterClub || inventory.have5IronClub;
        if (!haveClubInHand)
        {
            return;
        }
        if (Input.GetKeyDown(interactKey))
        {
            if (!mInitialized)
            {
                GameObject objectToShoot = raycastToObject();
                mStrength = 0f;
                if (objectToShoot)
                {
                    mObj = objectToShoot;
                }
                mInitialized = true;
            }
        }
        if (mInitialized)
        {
            mStrength += Time.deltaTime;
        }
        if ((mInitialized && Input.GetKeyUp(interactKey)) || mStrength > 2.0)
        {
            if (mObj == null)
            {
                mInitialized = false;
                return;
            }
            mStrength = Mathf.Clamp(mStrength, 0f, 2f);
            mInitialized = false;
            float strength = minStrength + (mStrength / 2f) * (maxStrength - minStrength);
            Vector3 direction;
            RaycastHit hit;
            Physics.Raycast(transform.position, cam.transform.forward, out hit);
            if (inventory.havePutterClub)
            {
                Debug.Log("Putter shot.");
                direction = new Vector3(transform.forward.x, 0, transform.forward.z);
                mObj.GetComponent<Rigidbody>().AddForce(direction.normalized * strength, ForceMode.Impulse);
            }
            if (inventory.have5IronClub)
            {
                Debug.Log("5 Iron shot.");
                direction = new Vector3(transform.forward.x, 1f, transform.forward.z);
                mObj.GetComponent<Rigidbody>().AddForce(direction.normalized * strength, ForceMode.Impulse);
            }
            mObj = null;
        }
    }
}
