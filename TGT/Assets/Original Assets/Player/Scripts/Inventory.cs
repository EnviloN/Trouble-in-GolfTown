using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public Text BallCountUI;

    [SerializeField] private int numOfBallsVar;

    public int numOfBalls {
        get => numOfBallsVar;
    }

    [SerializeField] private bool havePutterClubVar;

    public bool havePutterClub {
        get => havePutterClubVar;
        set => havePutterClubVar = value;
    }

    [SerializeField] private bool have5IronClubVar;

    public bool have5IronClub {
        get => have5IronClubVar;
        set => have5IronClubVar = value;
    }

    void Start() {
        resetInventory();
    }

    #region Inventory methods

    public void resetInventory() {
        havePutterClubVar = false;
        have5IronClubVar = false;
        numOfBallsVar = 0;
        UpdateUI();
    }

    #endregion

    #region Balls methods

    public bool haveBall() {
        return numOfBallsVar > 0;
    }

    public void addBall() {
        numOfBallsVar++;
        UpdateUI();
    }

    public bool removeBall() {
        if (!haveBall()) {
            return false;
        }

        numOfBallsVar--;
        UpdateUI();
        return true;
    }

    public void removeAllBalls() {
        numOfBallsVar = 0;
        UpdateUI();
    }

    private void UpdateUI() {
        BallCountUI.text = numOfBalls.ToString();
    }

#endregion
}