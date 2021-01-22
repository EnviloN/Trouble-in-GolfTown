using UnityEngine;

public class KeyboardPlayerInventory : AbstractInventory
{
    public KeyCode cancelKey;
    public KeyCode switchClubKey;
    public KeyCode showBallKey;
    public KeyCode interactKey;

    protected float relativeClubDistance = 0.3f;
    protected Quaternion clubRotation = Quaternion.Euler(-140.87f, -31.7f, 22f);

    override protected void Start()
    {
        initInventory();
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (Input.GetKeyDown(cancelKey))
        {
            RemoveClub();
            CancelRaycast();
        }

        // Club in hand
        if (Input.GetKeyDown(switchClubKey))
        {
            switch (clubInHandState)
            {
                case 0: // Neither of clubs is in player hand
                    if (havePutterClub)
                    {
                        InstantiatePutter();
                        clubInHandState = 1;
                    }
                    else if (have5IronClub)
                    {
                        InstantiateFiveIron();
                        clubInHandState = 2;
                    }
                    break;
                case 1: // Putter in player's hand
                    HideClub();
                    if (have5IronClub)
                    {
                        InstantiateFiveIron();
                        clubInHandState = 2;
                    }
                    else
                    {
                        clubInHandState = 0;
                    }
                    break;
                case 2: // 5Iron in player's hand
                    HideClub();
                    clubInHandState = 0;
                    break;
            }
        }


        // Ball raycasting
        if (raycasting)
        {
            RaycastBallHere();
        }

        if (Input.GetKeyDown(showBallKey))
        {
            if (raycasting)
            {
                CancelRaycast();
            }
            else if (haveBall() && CanPlaceBallHere())
            {
                raycasting = true;
            }
        }

        if (Input.GetKeyDown(interactKey))
        {
            tryPickupBall();
        }
    }

    protected void LateUpdate()
    {
        PositionClubInPlayersHand();
    }


    // Balls
    protected override bool doRaycastPickBall(out RaycastHit[] hits, float interactionDistance = DEFAULT_INTERACTION_DISTANCE)
    {
        hits = Physics.RaycastAll(GetRay(), interactionDistance);

        return hits.Length > 0;
    }

    protected override bool doRaycastPlaceBall(out RaycastHit raycastHit, float interactionDistance = DEFAULT_INTERACTION_DISTANCE)
    {
        return Physics.Raycast(GetRay(), out raycastHit, interactionDistance);
    }

    protected Ray GetRay()
    {
        return Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
    }

    // Clubs
    protected void PositionClubInPlayersHand()
    {
        if (clubInHandState == 1 && putterObject != null)
        {
            putterObject.transform.position = transform.position + (transform.right * relativeClubDistance);
            putterObject.transform.rotation = transform.rotation * clubRotation;
        } 
        if (clubInHandState == 2 && fiveIronObject != null)
        {
            fiveIronObject.transform.position = transform.position + (transform.right * relativeClubDistance);
            fiveIronObject.transform.rotation = transform.rotation * clubRotation;
        }
    }

    protected override void InstantiatePutter()
    {
        InstantiateClub(putterPrefab, ref putterObject, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
    }

    protected override void InstantiateFiveIron()
    {
        InstantiateClub(fiveIronPrefab, ref fiveIronObject, transform.position + (transform.right * relativeClubDistance), transform.rotation * clubRotation);
    }
}
