using UnityEngine;
using UnityEngine.UIElements;

public class BallSpawnLocation : MonoBehaviour {
    [Range(0f, 100f)]
    [Tooltip("In %.")]
    public float SpawnPropability = 33f;

    void Start() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Refresh(GameObject prefab) {
        if (Random.value < SpawnPropability / 100) {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
