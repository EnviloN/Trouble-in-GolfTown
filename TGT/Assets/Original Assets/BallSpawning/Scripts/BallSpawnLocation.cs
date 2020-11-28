using UnityEngine;

public class BallSpawnLocation : MonoBehaviour {
    [Range(0f, 100f)]
    [Tooltip("In %.")]
    public float spawnPropability = 33f;

    void Start() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Refresh(GameObject prefab) {
        if (Random.value < spawnPropability / 100) {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
