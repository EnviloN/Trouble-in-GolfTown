using UnityEngine;

public class GameStatus : MonoBehaviour {
    [SerializeField] private bool somethingHappenedVar;
    public bool somethingHappened {
        get => somethingHappenedVar;
        set => somethingHappenedVar = value;
    }

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

    #region Golden Balls
    [SerializeField]
    [Range(0, 3)]
    private int goldenBallsCollectedVar;
    public int goldenBallsCollected
    {
        get => goldenBallsCollectedVar;
        set => goldenBallsCollectedVar = value;
    }

    public void addGoldenBall()
    {
        if (goldenBallsCollectedVar != maxNumberOfGoldenBalls())
        {
            goldenBallsCollectedVar++;
        }
    }

    public void resetGoldenBallsCollected()
    {
        goldenBallsCollectedVar = 0;
    }

    public int maxNumberOfGoldenBalls()
    {
        return 3;
    }

    public bool haveCollectedAllGoldenBalls()
    {
        return goldenBallsCollectedVar == maxNumberOfGoldenBalls();
    }
    #endregion

    public object this[string propertyName] {
        get => this.GetType().GetProperty(propertyName)?.GetValue(this, null);
        set => this.GetType().GetProperty(propertyName)?.SetValue(this, value, null);
    }
}
