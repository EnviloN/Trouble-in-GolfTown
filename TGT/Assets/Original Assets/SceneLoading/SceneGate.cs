using UnityEngine;

public class SceneGate : MonoBehaviour {
    protected SceneLoader SceneLoader;
    protected GameManager GM;

    [SerializeField] protected Vector3 warpPosition;

    // Start is called before the first frame update
    void Start() {
        SceneLoader = FindObjectOfType<SceneLoader>();
        GM = FindObjectOfType<GameManager>();
    }

    public virtual void LoadScene() {}
}
