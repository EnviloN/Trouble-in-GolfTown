using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public Text BallCountUI;

    [SerializeField] private int numOfBallsVar;

    public KeyCode cancelKey;

    // Clubs in hand
    public KeyCode switchClubKey;
    public GameObject putterPrefab;
    public GameObject fiveIronPrefab;
    [SerializeField] private int clubInHandState = 0;
    private GameObject clubObject = null;
    private float relativeClubDistance = 0.3f;
    private Quaternion clubRotation = Quaternion.Euler(-145f, 20f, 0);

    // Ball in hand
    public KeyCode showPlaceBallKey;
    private float maxBallSpawnDistance = 4f;
    public GameObject spawnableGolfBallPrefab;
    [SerializeField] private bool raycasting = false;
    private GameObject ballObject = null;
    private float moveUpBy = 0.023f;

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
        if (Input.GetKeyDown(cancelKey))
        {
            RemoveClubFromHand();
            CancelRaycast();
        }

        if (Input.GetKeyDown(switchClubKey))
        {
            switch(clubInHandState)
            {
                case 0: // Neither of clubs is in player hand
                    if (havePutterClub)
                    {
                        InstantiatePutter();
                        clubInHandState = 1;
                    } else if (have5IronClub)
                    {
                        InstantiateFiveIron();
                        clubInHandState = 2;
                    }
                    break;
                case 1: // Putter in player's hand
                    Destroy(clubObject);

                    if (have5IronClub)
                    {
                        InstantiateFiveIron();
                        clubInHandState = 2;
                    } else
                    {
                        clubInHandState = 0;
                    }
                    break;
                case 2: // 5Iron in player's hand
                    Destroy(clubObject);
                    clubInHandState = 0;
                    break;
            }
        }

        if (raycasting)
        {
            RaycastBallHere();
        }

        if (Input.GetKeyDown(showPlaceBallKey))
        {
            if (raycasting)
            {
                CancelRaycast(false);
            } else if (haveBall() && CanPlaceBallHere())
            {
                raycasting = true;
                removeBall();
            }
        }
    }

    private void LateUpdate()
    {
        PositionClubInPlayersHand();
    }

    private Ray GetRay()
    {
        return Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
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

    private void InstantiatePutter()
    {
        clubObject = Instantiate(putterPrefab, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
    }

    private void InstantiateFiveIron()
    {
        clubObject = Instantiate(fiveIronPrefab, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
    }

    private void InstantiateGolfBall(Vector3 position)
    {
        ballObject = Instantiate(spawnableGolfBallPrefab, position + new Vector3(0, moveUpBy, 0), Quaternion.Euler(0, 0, 0));
    }

    private void MoveGolfBall(Vector3 toPosition)
    {
        if (ballObject != null)
        {
            ballObject.transform.SetPositionAndRotation(toPosition + new Vector3(0, moveUpBy, 0), Quaternion.Euler(0, 0, 0));
        }
    }

    public bool IsRaycastingBall()
    {
        return raycasting;
    }

    private bool CanPlaceBallHere()
    {
        var ray = GetRay();

        if (Physics.Raycast(ray, out var simpleHit, maxBallSpawnDistance))
        {
            return simpleHit.collider.GetComponent<GolfBallPlaceableArea>() != null;

        }
        return false;
    }

    private void RaycastBallHere()
    {
        var ray = GetRay();

        if (Physics.Raycast(ray, out var simpleHit, maxBallSpawnDistance))
        {
            var placeableArea = simpleHit.collider.GetComponent<GolfBallPlaceableArea>();
            if (placeableArea)
            {
                if (ballObject != null)
                {
                    MoveGolfBall(simpleHit.point);
                }
                else
                {
                    InstantiateGolfBall(simpleHit.point);
                }
            }
        }
    }

    public void CancelRaycast(bool addBallBackToInventory = true)
    {
        raycasting = false;
        if (addBallBackToInventory && ballObject != null)
        {
            Destroy(ballObject.gameObject);
            addBall();
        }
        ballObject = null;
    }

    public void RemoveClubFromHand()
    {
        if (clubInHandState != 0)
        {
            Destroy(clubObject);
            clubInHandState = 0;
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