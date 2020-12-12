using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public Text BallCountUI;

    [SerializeField] private int numOfBallsVar;

    public KeyCode switchClubKey;
    public GameObject putterPrefab;
    public GameObject fiveIronPrefab;
    private int clubInHandState = 0;
    private GameObject clubObject = null;
    private float relativeClubDistance = 0.3f;
    private Quaternion clubRotation = Quaternion.Euler(35f, 20f, 180f);

    public KeyCode switchBallKey;
    public float maxBallSpawnDistance = 3f;
    public GameObject spawnableGolfBallPrefab;
    private bool ballSpawningState = false;

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

    private void Update()
    {
        if (Input.GetKeyDown(switchClubKey))
        {
            switch(clubInHandState)
            {
                case 0: // Neither of clubs is in player hand
                    // Instantiate Putter in player's hand
                    clubObject = Instantiate(putterPrefab, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
                    clubInHandState = 1;
                    break;
                case 1: // Putter in player's hand
                    // Remove Putter from player's hand and instatiate 5Iron
                    Destroy(clubObject);
                    clubObject = Instantiate(fiveIronPrefab, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
                    clubInHandState = 2;
                    break;
                case 2: // 5Iron in player's hand
                    // Remove 5Iron from player's hand
                    Destroy(clubObject);
                    clubInHandState = 0;
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        PositionClubInPlayersHand();
    }

    #region ItemsInHand methods

    private void PositionClubInPlayersHand()
    {
        if (clubObject != null)
        {
            clubObject.transform.position = transform.position + (transform.right * relativeClubDistance);
            clubObject.transform.rotation = transform.rotation * clubRotation;

        }
    }

    #endregion

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