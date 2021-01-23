using UnityEngine;

public class Desctructible : MonoBehaviour
{
    public GameObject destroyedTower;
    public ParticleSystem smoke;

    public XRInteractions interactions;
    public float lifetime = 30.0f;
    private int smoke_number = 7;

    protected GameManager gameManager;
    public string towerId;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Check if should be destroyed already
        if (gameManager.destroyedTowers.Contains(towerId))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hasShootable = (other.GetComponent<Shootable>() != null);
        if (hasShootable)
        {
            gameManager.towerHitTriggered.Invoke(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    public void destroyThisTower()
    {
        Instantiate(destroyedTower, transform.position, transform.rotation);

        ParticleSystem[] clones = new ParticleSystem[smoke_number + 1];

        for (var i = 0; i < smoke_number; i++)
        {
            clones[i] = Instantiate(smoke, transform.position + Vector3.right * Random.Range(-8, 8) + Vector3.down * Random.Range(-2, 10) + Vector3.back * Random.Range(-7, 7), smoke.transform.rotation);
        }
        clones[smoke_number] = Instantiate(smoke, transform.position + Vector3.down, smoke.transform.rotation);

        Destroy(this.gameObject);
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            destroyThisTower();
        }
    }
    */
}
