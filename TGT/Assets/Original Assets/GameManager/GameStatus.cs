using UnityEngine;

public class GameStatus : MonoBehaviour {
    [SerializeField] private bool somethingHappenedVar;
    public bool somethingHappened {
        get => somethingHappenedVar;
        set => somethingHappenedVar = value;
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
    private int tutorialStageVar = 0;
    public int tutorialStage {
        get => tutorialStageVar;
        set => tutorialStageVar = value;
    }
    #endregion

    public object this[string propertyName] {
        get => this.GetType().GetProperty(propertyName)?.GetValue(this, null);
        set => this.GetType().GetProperty(propertyName)?.SetValue(this, value, null);
    }
}
