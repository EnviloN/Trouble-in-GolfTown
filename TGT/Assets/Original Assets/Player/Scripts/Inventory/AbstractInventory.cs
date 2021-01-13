using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractInventory : MonoBehaviour
{
    public const float DEFAULT_INTERACTION_DISTANCE = 4f;

    public Text BallCountUI;

    [SerializeField] protected int numOfBallsVar;

    // Prefabs
    public GameObject putterPrefab;
    public GameObject fiveIronPrefab;
    public GameObject spawnableGolfBallPrefab;
    public GameObject placeholderGolfBallPrefab;

    // Club in hand
    public int clubInHandState = 0;
    protected GameObject clubObject = null;

    // Ball in hand
    protected bool raycasting = false;
    protected GameObject ballObject = null;
    protected float moveUpBy = 0.03f;

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

    abstract protected bool doRaycast(out RaycastHit raycastHit, float interactionDistance = DEFAULT_INTERACTION_DISTANCE);

    protected void tryPickupBall()
    {
        // Pick ball from ground
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

    #region ItemsInHand methods

    abstract protected void InstantiatePutter();

    abstract protected void InstantiateFiveIron();

    protected void InstantiateClub(GameObject clubPrefab, Vector3 position, Quaternion rotation)
    {
        clubObject = Instantiate(clubPrefab, position, rotation);
    }

    protected void InstantiatePlaceholderBall(Vector3 position)
    {
        ballObject = Instantiate(placeholderGolfBallPrefab, position + new Vector3(0, 0.02f, 0), Quaternion.Euler(0, 0, 0));
    }

    protected void MovePlaceholderBall(Vector3 toPosition)
    {
        if (ballObject != null)
        {
            ballObject.transform.SetPositionAndRotation(toPosition + new Vector3(0, 0.02f, 0), Quaternion.Euler(0, 0, 0));
        }
    }

    protected void ReplacePlaceholderBallWithNormal()
    {
        if (ballObject != null)
        {
            Instantiate(spawnableGolfBallPrefab, ballObject.transform.position + new Vector3(0, moveUpBy, 0), ballObject.transform.rotation);
            Destroy(ballObject.gameObject);
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

    protected virtual void RaycastBallHere()
    {
        if (doRaycast(out RaycastHit raycastHit))
        {
            var placeableArea = raycastHit.collider.GetComponent<GolfBallPlaceableArea>();
            if (placeableArea)
            {
                if (ballObject != null)
                {
                    MovePlaceholderBall(raycastHit.point);
                }
                else
                {
                    InstantiatePlaceholderBall(raycastHit.point);
                }
            }
        }
    }

    public virtual void CancelRaycast(bool addBallBackToInventory = true)
    {
        if (raycasting)
        {
            raycasting = false;
            if (addBallBackToInventory && ballObject != null)
            {
                Destroy(ballObject.gameObject);
            }
            else if (ballObject != null)
            {
                ReplacePlaceholderBallWithNormal();
                removeBall();
            }
            ballObject = null;
        }
    }

    public void RemoveClub()
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