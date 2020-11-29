using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // ball prefab to be instantiated
    public GameObject goldenBallPrefab; // golden ball prefab to be instantiated

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
        // TODO Uncomment
        //int[] goldenBallLocations = new int[numOfGBToGenerate];
        // TODO Remove
        int[] goldenBallLocations = new int[3] { 0, 1, 2 };

        var locations = FindObjectsOfType<BallSpawnLocation>();
        int numOfLocations = locations.Length;

        // Generate locations for golden balls
        // TODO Uncomment
        /*for (int i = 0; i < numOfGBToGenerate; i++)
        {
            int locationNum = Random.Range(0, numOfLocations);
            while (Array.Exists(goldenBallLocations, e => e == locationNum))
            {
                locationNum = Random.Range(0, numOfLocations);
            }

            print("Golden ball on location num " + locationNum);
            goldenBallLocations[i] = locationNum;
        }
        */


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
