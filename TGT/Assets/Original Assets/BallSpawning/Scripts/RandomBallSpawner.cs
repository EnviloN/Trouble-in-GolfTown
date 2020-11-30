using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // ball prefab to be instantiated
    public GameObject goldenBallPrefab; // golden ball prefab to be instantiated
    public bool spawnGoldenBalls = false;

    private GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        RefreshSpawns();
    }

    // When called, all spawn locations are refreshed.
    public void RefreshSpawns()
    {
        int numOfGBToGenerate = gameStatus.maxNumberOfGoldenBalls() - gameStatus.goldenBallsCollected;
        int[] goldenBallLocations = new int[numOfGBToGenerate];

        var locations = FindObjectsOfType<BallSpawnLocation>();
        int numOfLocations = locations.Length;

        if (spawnGoldenBalls)
        {
            // Generate locations for golden balls
            for (int i = 0; i < numOfGBToGenerate; i++) {
                int locationNum = Random.Range(0, numOfLocations);
                while (Array.Exists(goldenBallLocations, e => e == locationNum)) {
                    locationNum = Random.Range(0, numOfLocations);
                }

                goldenBallLocations[i] = locationNum;
            }
        }

        // Place balls to world
        for (int i = 0; i < numOfLocations; i++)
        {
            if (Array.Exists(goldenBallLocations, e => e == i))
            {
                locations[i].Refresh(goldenBallPrefab, true);
            }
            else
            {
                locations[i].Refresh(ballPrefab);
            }
        }
    }
}
