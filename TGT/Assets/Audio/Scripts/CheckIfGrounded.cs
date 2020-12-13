using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGrounded : MonoBehaviour
{

    public Collider playerCollider;
    public bool isGrounded;
    public bool isOnTerrain;
    public bool isOnStructure;

    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        isGrounded = PlayerGrounded();
        isOnTerrain = CheckOnTerrain();
        isOnStructure = CheckForStructure();
        
    }

    bool PlayerGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, out hit, playerCollider.bounds.extents.y + 1.0f);
    }

    bool CheckOnTerrain() {
        if (hit.collider != null && hit.collider.tag == "Terrain") //this is probably faulty logic
        {
            return true;
        }
        else return false;
    }

    bool CheckForStructure()
    {
        if (hit.collider != null && hit.collider.tag == "Structure")
        {
            return true;
        }
        else return false;
    }


}
