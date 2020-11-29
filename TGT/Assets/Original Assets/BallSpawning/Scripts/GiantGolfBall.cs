using UnityEngine;

public class GiantGolfBall : MonoBehaviour
{
    void Start()
    {
        if (!FindObjectOfType<GameStatus>().haveCollectedAllGoldenBalls()) {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
    }
}
