using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool debugMode;

    private GameStatus gameStatus;
    private DialogueManager dialogueManager;
    private GameObject player;

    [NonSerialized]
    public int MaxNumberOfGoldenBalls = 3;

    // Start is called before the first frame update
    void Awake() {
        gameStatus = FindObjectOfType<GameStatus>(); // Game status should be created here and should not be a mono behavior
        dialogueManager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetGameStatus(string property, object value) {
        gameStatus[property] = value;
        dialogueManager.UpdateGraphs();
    }

    public object GetGameStatus(string property) {
        return gameStatus[property];
    }

    #region Golden Balls
    public void AddGoldenBall() {
        var currentGoldenBallCount = (int) GetGameStatus("goldenBallsCollected");
        if (currentGoldenBallCount == MaxNumberOfGoldenBalls) return;
        currentGoldenBallCount++;
        SetGameStatus("goldenBallsCollected", currentGoldenBallCount);
    }

    public void ResetGoldenBallsCollected() {
        SetGameStatus("goldenBallsCollected", 0);
    }

    public int GetGoldenBallsCollected() {
        return (int) GetGameStatus("goldenBallsCollected");
    }

    public bool HaveCollectedAllGoldenBalls() {
        return ((int) GetGameStatus("goldenBallsCollected") == MaxNumberOfGoldenBalls);
    }
    #endregion
}
