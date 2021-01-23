using UnityEngine;

public class MagnateHitEventHandler : MonoBehaviour
{
    protected GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hasShootable = (other.GetComponent<Shootable>() != null);
        if (hasShootable)
        {
            gameManager.magnateHitTriggered.Invoke(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    public void hideMagnate()
    {
        this.gameObject.SetActive(false);
    }
}