using UnityEngine;

public class KeyboardPlayerInventory : AbstractInventory
{
    public KeyCode cancelKey;
    public KeyCode showBallKey;
    public KeyCode interactKey;

    public KeyCode clubsEmptyHands;
    public KeyCode clubsPutter;
    public KeyCode clubsFiveiron;

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
        if (getMouseWheelInput() > 0)
        {
            switch (clubInHandState)
            {
                case 0: // Neither of clubs is in player hand
                    if (havePutterClub)
                    {
                        goToClubState(1);
                    }
                    else if (have5IronClub)
                    {
                        goToClubState(2);
                    }
                    break;
                case 1: // Putter in player's hand
                    if (have5IronClub)
                    {
                        goToClubState(2);
                    }
                    else
                    {
                        goToClubState(0);
                    }
                    break;
                case 2: // 5Iron in player's hand
                    goToClubState(0);
                    break;
            }
        }

        if (getMouseWheelInput() < 0)
        {
            switch (clubInHandState)
            {
                case 0: // Neither of clubs is in player hand
                    if (have5IronClub)
                    {
                        goToClubState(2);
                    }
                    else if (havePutterClub)
                    {
                        goToClubState(1);
                    }
                    break;
                case 1: // Putter in player's hand
                    goToClubState(0);
                    break;
                case 2: // 5Iron in player's hand
                    if (havePutterClub)
                    {
                        goToClubState(1);
                    } else
                    {
                        goToClubState(0);
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(clubsEmptyHands))
        {
            goToClubState(0);
        }

        if (Input.GetKeyDown(clubsPutter))
        {
            goToClubState(1);
        }

        if (Input.GetKeyDown(clubsFiveiron))
        {
            goToClubState(2);
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

    protected float getMouseWheelInput()
    {
        return Input.mouseScrollDelta.y;
    }

    protected void goToClubState(int state)
    {
        if (clubInHandState == state)
        {
            return;
        }
        switch(state)
        {
            case 0:
                HideClub();
                clubInHandState = 0;
                break;
            case 1:
                if (!havePutterClub)
                {
                    break;
                }
                HideClub();
                InstantiatePutter();
                clubInHandState = 1;
                break;
            case 2:
                if (!have5IronClub)
                {
                    break;
                }
                HideClub();
                InstantiateFiveIron();
                clubInHandState = 2;
                break;
        }
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
