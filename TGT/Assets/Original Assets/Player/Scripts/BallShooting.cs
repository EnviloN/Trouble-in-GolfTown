using System.Collections;
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
    Color               mStartingColor;

    public float maxStrength;
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
        Physics.Raycast(transform.position, cam.transform.forward, out hit);
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
                if (objectToShoot)
                {
                    mObj = objectToShoot;
                    mStartingColor = mObj.GetComponent<Material>().color;
                }
                mInitialized = true;
            } 
            else
            {
                mStrength += Time.deltaTime;
                Color color = new Color(0.5f * (mStrength), mStartingColor.g, mStartingColor.b, mStartingColor.a);
                mObj.GetComponent<Material>().color = color;
            }

        } 
        if ((mInitialized && Input.GetKeyUp(interactKey)) || mStrength > 2.0)
        {
            mStrength = Mathf.Clamp(mStrength, 0f, 2f);
            mInitialized = false;
            float strength = minStrength + ((maxStrength - minStrength) / (2f)) * (mStrength);
            mObj.GetComponent<Material>().color = mStartingColor;
            Vector3 direction;
            RaycastHit hit;
            Physics.Raycast(transform.position, cam.transform.forward, out hit);
            if (inventory.havePutterClub)
            {
                direction = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
                mObj.GetComponent<Rigidbody>().AddForce(direction * strength);
            }
            if (inventory.have5IronClub)
            {
                Vector3 reflect = Vector3.Reflect(cam.transform.forward, Vector3.up);
                direction = reflect;
                mObj.GetComponent<Rigidbody>().AddForce(direction * strength);
            }
        }
    }
}
