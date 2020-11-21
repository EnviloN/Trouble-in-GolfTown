using UnityEngine;

public class RandomBallSpawner : MonoBehaviour {
    private BallSpawnLocation[] locations;

    public GameObject ballPrefab; // ball prefab to be instantiated

    void Start() {
        // Get all spawn locations
        locations = FindObjectsOfType<BallSpawnLocation>();
        RefreshSpawns();
    }

    // When called, all spawn locations are refreshed.
    public void RefreshSpawns() {
        foreach (BallSpawnLocation location in locations) {
            location.Refresh(ballPrefab);
        }
    }
}
