using UnityEngine;
using UnityEngine.UI;

public abstract class Inventory : MonoBehaviour
{
    public const float DEFAULT_INTERACTION_DISTANCE = 2f;

    public Text BallCountUI;

    [SerializeField] protected int numOfBallsVar;

    // Prefabs
    public GameObject putterPrefab;
    public GameObject fiveIronPrefab;
    public GameObject spawnableGolfBallPrefab;

    // Club in hand
    protected int clubInHandState = 0;
    protected GameObject clubObject = null;
    protected float relativeClubDistance = 0.3f;
    protected Quaternion clubRotation = Quaternion.Euler(-145f, 20f, 0);

    // Ball in hand
    protected bool raycasting = false;
    protected GameObject ballObject = null;
    protected float moveUpBy = 0.023f;

    // Others
    protected GameManager gm;
    protected GiantGolfBall giantBall;

    public int numOfBalls
    {
        get => numOfBallsVar;
    }

    [SerializeField] protected bool havePutterClubVar;

    public bool havePutterClub
    {
        get => havePutterClubVar;
        set => havePutterClubVar = value;
    }

    [SerializeField] protected bool have5IronClubVar;

    public bool have5IronClub
    {
        get => have5IronClubVar;
        set => have5IronClubVar = value;
    }

    abstract protected void Start();

    abstract protected void Update();

    protected void LateUpdate()
    {
        PositionClubInPlayersHand();
    }

    abstract protected bool interactKeyPressed();

    abstract protected bool doRaycast(out RaycastHit raycastHit, float interactionDistance = DEFAULT_INTERACTION_DISTANCE);

    protected void tryPickupBall()
    {
        // Pick ball from ground
        if (interactKeyPressed())
        {
            if (doRaycast(out RaycastHit raycastHit))
            {
                Placeholder ball = raycastHit.collider.GetComponent<Placeholder>();
                GoldenBallPlaceholder goldenBall = raycastHit.collider.GetComponent<GoldenBallPlaceholder>();

                if (!ball && !goldenBall) return;

                if (ball)
                {
                    if (raycasting)
                    {
                        // Place ball on the ground
                        CancelRaycast(false);
                    }
                    else
                    {
                        // Pick ball and add it to inventory
                        ball.gameObject.SetActive(false);
                        addBall();
                        var count = (int)gm.GetGameStatus("ballsCollected") + 1;
                        gm.SetGameStatus("ballsCollected", count);
                    }
                    return;
                }
                if (goldenBall)
                {
                    goldenBall.gameObject.SetActive(false);
                    addBall();
                    gm.AddGoldenBall();
                    if (gm.HaveCollectedAllGoldenBalls())
                    {
                        giantBall.GetComponent<MeshRenderer>().enabled = true;
                        giantBall.GetComponent<MeshCollider>().enabled = true;
                    }
                    return;
                }
            }
        }
    }

    #region ItemsInHand methods

    protected void PositionClubInPlayersHand()
    {
        if (clubObject != null)
        {
            clubObject.transform.position = transform.position + (transform.right * relativeClubDistance);
            clubObject.transform.rotation = transform.rotation * clubRotation;

        }
    }

    protected void InstantiatePutter()
    {
        clubObject = Instantiate(putterPrefab, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
    }

    protected void InstantiateFiveIron()
    {
        clubObject = Instantiate(fiveIronPrefab, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
    }

    protected void InstantiateGolfBall(Vector3 position)
    {
        ballObject = Instantiate(spawnableGolfBallPrefab, position + new Vector3(0, moveUpBy, 0), Quaternion.Euler(0, 0, 0));
    }

    protected void MoveGolfBall(Vector3 toPosition)
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

    protected bool CanPlaceBallHere()
    {
        if (doRaycast(out RaycastHit raycastHit))
        {
            return raycastHit.collider.GetComponent<GolfBallPlaceableArea>() != null;

        }
        return false;
    }

    protected void RaycastBallHere()
    {
        if (doRaycast(out RaycastHit raycastHit))
        {
            var placeableArea = raycastHit.collider.GetComponent<GolfBallPlaceableArea>();
            if (placeableArea)
            {
                if (ballObject != null)
                {
                    MoveGolfBall(raycastHit.point);
                }
                else
                {
                    InstantiateGolfBall(raycastHit.point);
                }
            }
        }
    }

    public void CancelRaycast(bool addBallBackToInventory = true)
    {
        if (raycasting)
        {
            raycasting = false;
            if (addBallBackToInventory && ballObject != null)
            {
                Destroy(ballObject.gameObject);
                addBall();
            }
            ballObject = null;
        }
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

    public void resetInventory()
    {
        havePutterClubVar = false;
        have5IronClubVar = false;
        numOfBallsVar = 0;
        UpdateUI();
    }

    protected void initInventory()
    {
        resetInventory();
        gm = FindObjectOfType<GameManager>();
        giantBall = FindObjectOfType<GiantGolfBall>();
    }

    #endregion

    #region Balls methods

    public bool haveBall()
    {
        return numOfBallsVar > 0;
    }

    public void addBall()
    {
        numOfBallsVar++;
        UpdateUI();
    }

    public bool removeBall()
    {
        if (!haveBall())
        {
            return false;
        }

        numOfBallsVar--;
        UpdateUI();
        return true;
    }

    public void removeAllBalls()
    {
        numOfBallsVar = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        BallCountUI.text = numOfBalls.ToString();
    }

    #endregion
}