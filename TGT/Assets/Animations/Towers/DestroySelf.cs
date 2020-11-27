using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
