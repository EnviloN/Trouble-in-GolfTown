using UnityEngine;

public class SceneGate : MonoBehaviour {
    protected SceneLoader SceneLoader;

    [SerializeField] protected Vector3 warpPosition;

    // Start is called before the first frame update
    void Start() {
        SceneLoader = FindObjectOfType<SceneLoader>();
    }

    public virtual void LoadScene() {}
}
