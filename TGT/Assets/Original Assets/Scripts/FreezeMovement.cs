using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FreezeMovement : MonoBehaviour
{

    void Start()
    {
        //UnFreeze();
    }


    public void Freeze() {

        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody) //PC Player
        {
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            //VR Player
            FindObjectOfType<DeviceBasedContinuousMoveProvider>().enabled = false;
            FindObjectOfType<DeviceBasedContinuousTurnProvider>().enabled = false;
        }

    }

    public void UnFreeze()
    {
        //PC Player
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            //VR Player
            FindObjectOfType<DeviceBasedContinuousMoveProvider>().enabled = true;
            FindObjectOfType<DeviceBasedContinuousTurnProvider>().enabled = true;
        }

    }
}

