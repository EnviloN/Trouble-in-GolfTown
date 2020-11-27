using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Desctructible : MonoBehaviour
{
    public GameObject destroyedTower;
    public ParticleSystem smoke;

    public float lifetime = 30.0f;
    private int smoke_number = 7;

    private GameObject tower;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            tower = Instantiate(destroyedTower, transform.position, transform.rotation);

            ParticleSystem[] clones = new ParticleSystem[smoke_number+1];
            
            for (var i = 0; i < smoke_number; i++) {
                clones[i] = Instantiate(smoke, transform.position + Vector3.right * Random.Range(-8,8) + Vector3.down * Random.Range(-2, 10) + Vector3.back * Random.Range(-7, 7), smoke.transform.rotation);
            }
            clones[smoke_number] = Instantiate(smoke, transform.position + Vector3.down , smoke.transform.rotation);

            Destroy(this.gameObject);
        }
    }

}
