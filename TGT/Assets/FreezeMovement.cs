using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMovement : MonoBehaviour
{

    //private bool isFrozen;
    // Start is called before the first frame update
    void Start()
    {
        //isFrozen = false;
        //UnFreeze();
    }


    public void Freeze() {
        //isFrozen = true;
        //PC Player
        Debug.Log("Freezing player movement");
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else {
            Debug.Log("else");
            //VR Player
            CharacterController controller = this.GetComponent<CharacterController>();
            controller.enabled = false;
        }

    }

    public void UnFreeze()
    {
        //isFrozen = false;
        Debug.Log("Restoring player movement");
        //PC Player
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            Debug.Log("else");
            //VR Player
            CharacterController controller = this.GetComponent<CharacterController>();
            controller.enabled = true;
        }

    }
}

