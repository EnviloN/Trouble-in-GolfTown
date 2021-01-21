﻿using UnityEngine;

public class GameStatus : MonoBehaviour {
    [SerializeField] private int timesShotVar;
    public int timesShot {
        get => timesShotVar;
        set => timesShotVar = value;
    }

    [SerializeField] private int ballsCollectedVar;
    public int ballsCollected {
        get => ballsCollectedVar;
        set => ballsCollectedVar = value;
    }

    [SerializeField]
    private int goldenBallsCollectedVar;
    public int goldenBallsCollected
    {
        get => goldenBallsCollectedVar;
        set => goldenBallsCollectedVar = value;
    }

    #region Quests
    [SerializeField]
    private int tutorialStageVar;
    public int tutorialStage {
        get => tutorialStageVar;
        set => tutorialStageVar = value;
    }

    [SerializeField]
    private int quest1StageVar;
    public int quest1Stage {
        get => quest1StageVar;
        set => quest1StageVar = value;
    }

    private int givePutterVar;
    public int givePutter {
        get => givePutterVar;
        set => givePutterVar = value;
    }

    private int give5IronVar;
    public int give5Iron {
        get => give5IronVar;
        set => give5IronVar = value;
    }
    #endregion

    #region Meetings
    private int metJoeVar;
    public int metJoe {
        get => metJoeVar;
        set => metJoeVar = value;
    }

    private int metDixxiVar;
    public int metDixxi {
        get => metDixxiVar;
        set => metDixxiVar = value;
    }

    private int metBillyVar;
    public int metBilly {
        get => metBillyVar;
        set => metBillyVar = value;
    }

    private int metJaneVar;
    public int metJane {
        get => metJaneVar;
        set => metJaneVar = value;
    }
    #endregion


    public object this[string propertyName] {
        get => this.GetType().GetProperty(propertyName)?.GetValue(this, null);
        set => this.GetType().GetProperty(propertyName)?.SetValue(this, value, null);
    }
}
