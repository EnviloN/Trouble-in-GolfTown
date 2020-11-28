using UnityEngine;

public class SceneGate : MonoBehaviour {
    protected SceneLoader SceneLoader;
    protected GameObject player;

    [SerializeField] protected Vector3 warpPosition;

    // Start is called before the first frame update
    void Start() {
        SceneLoader = FindObjectOfType<SceneLoader>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void LoadScene() {
        player.transform.position = warpPosition;
    }
}
