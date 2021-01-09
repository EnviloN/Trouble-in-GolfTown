using UnityEngine;

public class KeyboardPlayerInventory : Inventory
{
    public KeyCode cancelKey;
    public KeyCode switchClubKey;
    public KeyCode showPlaceBallKey;
    public KeyCode interactKey;

    override protected void Start()
    {
        initInventory();
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (Input.GetKeyDown(cancelKey))
        {
            RemoveClubFromHand();
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
                    Destroy(clubObject);

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
                    Destroy(clubObject);
                    clubInHandState = 0;
                    break;
            }
        }


        // Ball raycasting
        if (raycasting)
        {
            RaycastBallHere();
        }

        if (Input.GetKeyDown(showPlaceBallKey))
        {
            if (raycasting)
            {
                CancelRaycast(true);
            }
            else if (haveBall() && CanPlaceBallHere())
            {
                raycasting = true;
                removeBall();
            }
        }

        tryPickupBall();
    }

    protected override bool interactKeyPressed()
    {
        return Input.GetKeyDown(interactKey);
    }

    protected override bool doRaycast(out RaycastHit raycastHit, float interactionDistance = DEFAULT_INTERACTION_DISTANCE)
    {
        return Physics.Raycast(GetRay(), out raycastHit, interactionDistance);
    }

    protected Ray GetRay()
    {
        return Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
    }
}
