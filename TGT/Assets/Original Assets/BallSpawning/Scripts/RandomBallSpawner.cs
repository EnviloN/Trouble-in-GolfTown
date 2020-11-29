using UnityEngine;
using Random = UnityEngine.Random;

public class RandomBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // ball prefab to be instantiated
    public GameObject goldenBallPrefab; // golden ball prefab to be instantiated

    [SerializeField]
    private GameStatus gameStatus;

    private void Start()
    {
        RefreshSpawns();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // When called, all spawn locations are refreshed.
    public void RefreshSpawns()
    {
        // Pick up to 3 random locations for golden golf ball (based on how many player picked up already)
        int numOfGBToGenerate = gameStatus.maxNumberOfGoldenBalls() - gameStatus.goldenBallsCollected;

        var locations = FindObjectsOfType<BallSpawnLocation>();
        foreach (BallSpawnLocation location in locations)
        {
            location.Refresh(ballPrefab);
        }

        if (numOfGBToGenerate > 0 && numOfGBToGenerate <= gameStatus.maxNumberOfGoldenBalls())
        {
            int numOfLocations = locations.Length;
            for (int i = 0; i < numOfGBToGenerate; i++)
            {
                // Random.Range -> Min inclusive, max exclusive: https://docs.unity3d.com/ScriptReference/Random.Range.html
                int locationNum = Random.Range(0, numOfLocations);
                locations[locationNum].Refresh(goldenBallPrefab);
            }

        }
    }
}
