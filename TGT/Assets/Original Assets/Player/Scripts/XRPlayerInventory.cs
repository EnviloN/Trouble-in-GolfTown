using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPlayerInventory : Inventory
{
    public XRInteractions interactions;
    protected XRRayInteractor rayInteractor;

    // Button presses
    private bool leftPrimaryPressed = false;
    private bool leftTriggerPressed = false;

    override protected void Start()
    {
        initInventory();
        rayInteractor = FindObjectOfType<XRRayInteractor>();
        interactions.leftPrimaryButtonPress.AddListener(pressed => {
            leftPrimaryPressed = pressed;
        });
        interactions.leftSecondaryButtonPress.AddListener(pressed => {
            if (raycasting)
            {
                CancelRaycast(true);
            }
            else if (haveBall() && CanPlaceBallHere())
            {
                raycasting = true;
                removeBall();
            }
        });
        interactions.leftTriggerButtonPress.AddListener(pressed => {
            leftTriggerPressed = pressed; 
        });
    }

    // Update is called once per frame
    override protected void Update()
    {
        /*if (leftPrimaryPressed)
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
        }*/

        if (raycasting)
        {
            RaycastBallHere();
        }

        if (leftTriggerPressed && raycasting)
        {
            CancelRaycast(false);
        }

        tryPickupBall();
    }

    protected override bool interactKeyPressed()
    {
        return leftTriggerPressed;
    }

    protected override bool doRaycast(out RaycastHit raycastHit, float interactionDistance = DEFAULT_INTERACTION_DISTANCE)
    {
        return rayInteractor.GetCurrentRaycastHit(out raycastHit);
    }
}
