using System;
using UnityEngine;

public class RandomBallSpawner : MonoBehaviour {

    public GameObject ballPrefab; // ball prefab to be instantiated

    private void Start() {
        RefreshSpawns();
    }

    // When called, all spawn locations are refreshed.
    public void RefreshSpawns() {
        var locations = FindObjectsOfType<BallSpawnLocation>();
        foreach (BallSpawnLocation location in locations) {
            location.Refresh(ballPrefab);
        }
    }
}
