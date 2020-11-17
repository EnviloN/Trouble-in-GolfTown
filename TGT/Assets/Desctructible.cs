using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desctructible : MonoBehaviour
{
    public GameObject destroyedTower;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(destroyedTower, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

}
