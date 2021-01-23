using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractInventory : MonoBehaviour
{
    public const float DEFAULT_INTERACTION_DISTANCE = 2.5f;

    public Text BallCountUI;

    [SerializeField] protected int numOfBallsVar;

    // Prefabs
    public GameObject putterPrefab;
    public GameObject fiveIronPrefab;
    public GameObject spawnableGolfBallPrefab;
    public GameObject placeholderGolfBallPrefab;

    // Club in hand
    public int clubInHandState = 0;
    protected GameObject putterObject = null;
    protected GameObject fiveIronObject = null;

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

    abstract protected bool doRaycastPickBall(out RaycastHit[] hits, float interactionDistance = DEFAULT_INTERACTION_DISTANCE);

    abstract protected bool doRaycastPlaceBall(out RaycastHit raycastHit, float interactionDistance = DEFAULT_INTERACTION_DISTANCE);

    protected void tryPickupBall()
    {
        // Pick ball from ground
        if (doRaycastPickBall(out RaycastHit[] raycastHits))
        {
            foreach(RaycastHit hit in raycastHits)
            {
                Placeholder ball = hit.collider.GetComponent<Placeholder>();
                GoldenBallPlaceholder goldenBall = hit.collider.GetComponent<GoldenBallPlaceholder>();

                if (!ball && !goldenBall) continue;

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

    abstract protected void InstantiatePutter();

    abstract protected void InstantiateFiveIron();

    protected void InstantiateClub(GameObject clubPrefab, ref GameObject clubObject, Vector3 position, Quaternion rotation)
    {
        if (clubObject == null) {
            clubObject = Instantiate(clubPrefab, position, rotation);
        } else {
            clubObject.transform.SetPositionAndRotation(position, rotation);
            clubObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            clubObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            MeshCollider collider = clubObject.GetComponent<MeshCollider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            clubObject.SetActive(true);
        }
    }

    public void HideClub()
    {
        if (putterObject != null && putterObject.activeInHierarchy) {
            putterObject.SetActive(false);
            MeshCollider collider = putterObject.GetComponent<MeshCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            putterObject.GetComponent<Rigidbody>().useGravity = false;
        }
        if (fiveIronObject != null && fiveIronObject.activeInHierarchy) {
            fiveIronObject.SetActive(false);
            MeshCollider collider = fiveIronObject.GetComponent<MeshCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            fiveIronObject.GetComponent<Rigidbody>().useGravity = false;
        }
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
            GameObject ball = Instantiate(spawnableGolfBallPrefab, ballObject.transform.position + new Vector3(0, moveUpBy, 0), ballObject.transform.rotation);
            Physics.IgnoreCollision(ball.GetComponent<Collider>(), GetComponent<Collider>());
            Destroy(ballObject.gameObject);
        }
    }

    public bool IsRaycastingBall()
    {
        return raycasting;
    }

    protected bool CanPlaceBallHere()
    {
        if (doRaycastPlaceBall(out RaycastHit raycastHit))
        {
            return raycastHit.collider.GetComponent<GolfBallPlaceableArea>() != null;

        }
        return false;
    }

    protected virtual void RaycastBallHere()
    {
        if (doRaycastPlaceBall(out RaycastHit raycastHit))
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
            HideClub();
            clubInHandState = 0;
        }
    }

    #endregion

    #region Inventory methods

    public void resetInventory()
    {
        //havePutterClubVar = false;
        //have5IronClubVar = false;
        //numOfBallsVar = 0;
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