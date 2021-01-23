using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooting : MonoBehaviour
{

    public KeyboardPlayerInventory inventory;
    public KeyCode      interactKey;
    public Camera       cam;

    private bool        mInitialized;
    float               mStrength;
    GameObject          mObj;

    public float       maxStrength;
    public float       minStrength;

    protected PowerBarController powerBar;

    // Start is called before the first frame update
    void Start()
    {
        mInitialized = false;
        mStrength = 0f;

        powerBar = FindObjectOfType<PowerBarController>();
    }

    private GameObject raycastToObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Shootable>())
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        bool haveClubInHand = inventory.clubInHandState != 0;
        if (!haveClubInHand)
        {
            return;
        }
        if ((mInitialized && Input.GetKeyUp(interactKey)) || mStrength > 2.0) {
            mObj = raycastToObject();
            if (mObj == null)
            {
                mInitialized = false;
                powerBar.SetBarToPercents(0f);
                return;
            }
            mStrength = Mathf.Clamp(mStrength, 0f, 2f);
            mInitialized = false;
            float strength = minStrength + (mStrength / 2f) * (maxStrength - minStrength);
            Vector3 direction;
            if (inventory.clubInHandState == 1)
            {
                direction = new Vector3(transform.forward.x, 0, transform.forward.z);
                mObj.GetComponent<Rigidbody>().AddForce(direction.normalized * strength, ForceMode.Impulse);
            }
            if (inventory.clubInHandState == 2)
            {
                direction = new Vector3(transform.forward.x, 1f, transform.forward.z);
                mObj.GetComponent<Rigidbody>().AddForce(direction.normalized * strength, ForceMode.Impulse);
            }
            mObj = null;
            powerBar.SetBarToPercents(0f);
            return;
        }
        if (mInitialized)
        {
            mStrength += Time.deltaTime;
            powerBar.SetBarToPercents(mStrength / (2f / 100));
            return;
        }
        if (Input.GetKeyDown(interactKey) && !mInitialized)
        {
            if (raycastToObject() != null)
            {
                mStrength = 0f;
                powerBar.SetBarToPercents(0f);
                mInitialized = true;
            }
            
        }
    }
}
