using UnityEngine;

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

    private int quest1TalkedVar;
    public int quest1Talked {
        get => quest1TalkedVar;
        set => quest1TalkedVar = value;
    }

    public int quest1StageBallsVar = 0;
    public int quest1StageBalls
    {
        get => quest1StageBallsVar;
        set => quest1StageBallsVar = value;
    }

    [SerializeField]
    private int quest2StageVar;
    public int quest2Stage {
        get => quest2StageVar;
        set => quest2StageVar = value;
    }

    private int quest2DixxiTalkVar;
    public int quest2DixxiTalk {
        get => quest2DixxiTalkVar;
        set => quest2DixxiTalkVar = value;
    }

    [SerializeField]
    private int numOfTowersDestroyedVar = 0;
    public int numOfTowersDestroyed {
        get => numOfTowersDestroyedVar;
        set => numOfTowersDestroyedVar = value;
    }

    [SerializeField]
    private int quest3StageVar;
    public int quest3Stage {
        get => quest3StageVar;
        set => quest3StageVar = value;
    }

    [SerializeField]
    private int gameEndVar;
    public int gameEnd {
        get => gameEndVar;
        set => gameEndVar = value;
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

    private int talkedToFVisitorVar;
    public int talkedToFVisitor {
        get => talkedToFVisitorVar;
        set => talkedToFVisitorVar = value;
    }

    private int talkedToMVisitorVar;
    public int talkedToMVisitor {
        get => talkedToMVisitorVar;
        set => talkedToMVisitorVar = value;
    }

    private int talkedToBeauVar;
    public int talkedToBeau {
        get => talkedToBeauVar;
        set => talkedToBeauVar = value;
    }

    private int talkedToAustinVar;
    public int talkedToAustin {
        get => talkedToAustinVar;
        set => talkedToAustinVar = value;
    }

    private int talkedToDixxiVar;
    public int talkedToDixxi {
        get => talkedToDixxiVar;
        set => talkedToDixxiVar = value;
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
