﻿using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool debugMode;

    private GameStatus gameStatus;
    private DialogueManager dialogueManager;
    private GameObject player;

    private float framesToUpdateGraphs = 1f;

    [NonSerialized]
    public int MaxNumberOfGoldenBalls = 3;

    public HoleTriggeredEvent holeTriggered;

    public TowerHitEvent towerHitTriggered;

    public MagnateHitEvent magnateHitTriggered;

    // Start is called before the first frame update
    void Awake() {
        gameStatus = FindObjectOfType<GameStatus>(); // Game status should be created here and should not be a mono behavior
        dialogueManager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (holeTriggered == null)
        {
            holeTriggered = new HoleTriggeredEvent();
        }

        if (towerHitTriggered == null)
        {
            towerHitTriggered = new TowerHitEvent();
        }

        if (magnateHitTriggered == null)
        {
            magnateHitTriggered = new MagnateHitEvent();
        }
    }

    private void Start()
    {
        // Use constants on MinigolfCourseIdMap when referencing courseIds
        holeTriggered.AddListener(courseId =>
        {
            if (courseId == MinigolfCourseIdMap.CemeteryCourse1 && gameStatus.tutorialStage == 4)
            {
                gameStatus.tutorialStage = 5;
            }

            if (gameStatus.quest1Stage == 10)
            {
                gameStatus.quest1StageBalls++;
                if (gameStatus.quest1StageBalls == 3)
                    gameStatus.quest2Stage = 1;
            }
        });

        towerHitTriggered.AddListener(towerObject =>
        {
            Desctructible destructible = towerObject.GetComponent<Desctructible>();
            if (destructible != null)
            {
                destructible.destroyThisTower();
                gameStatus.numOfTowersDestroyed++;
            }
        });

        magnateHitTriggered.AddListener((magnateObject) =>
        {
            if (gameStatus.quest3Stage == 1 || debugMode) {
                gameStatus.quest3Stage++;
                gameStatus.gameEnd = 1;
                //magnateObject.GetComponent<MagnateHitEventHandler>().hideMagnate();
                Debug.Log("HIT!");
                // Pause, open end menu, play explosion and music
            }

        });
    }

    private void Update() {
        // Nothing to see here, move along...

        if (gameStatus.givePutter == 1) {
            gameStatus.givePutter = 0;
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<AbstractInventory>().havePutterClub = true;
        }

        if (gameStatus.give5Iron == 1) {
            gameStatus.give5Iron = 0;
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<AbstractInventory>().have5IronClub = true;
        }
        
        if (framesToUpdateGraphs <= 0) {
            // Hot fix of the story, can be fixed in dialogues
            if (gameStatus.quest1Talked == 3) {
                gameStatus.quest1Talked += 1;
                gameStatus.quest1Stage = 8;
            }

            gameStatus.ballsCollected = player.GetComponent<AbstractInventory>().numOfBalls;

            dialogueManager.UpdateGraphs();
            framesToUpdateGraphs = 1f;
        }

        framesToUpdateGraphs -= Time.deltaTime;
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
