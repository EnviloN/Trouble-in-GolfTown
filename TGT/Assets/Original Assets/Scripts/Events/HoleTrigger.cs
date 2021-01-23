using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    [SerializeField] protected string courseIdVar;
    public string courseId
    {
        get => courseIdVar;
    }

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
            gameManager.holeTriggered.Invoke(courseId);
        }
    }
}
