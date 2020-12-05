using UnityEngine;

public class GiantGolfBall : MonoBehaviour
{
    void Start()
    {
        if (!FindObjectOfType<GameManager>().HaveCollectedAllGoldenBalls()) {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
    }
}
