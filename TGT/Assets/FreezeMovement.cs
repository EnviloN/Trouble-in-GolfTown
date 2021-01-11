using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else
        { //VR Player
            //Continouous movement
            //gameObject.GetComponent<DeviceBasedContinuousMoveProvider>().enabled = false;
            CharacterController controller = this.GetComponent<CharacterController>();
            controller.enabled = false;
        }

    }

    public void UnFreeze()
    {
        //PC Player
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            //VR Player
            CharacterController controller = this.GetComponent<CharacterController>();
            controller.enabled = true;
        }

    }
}

