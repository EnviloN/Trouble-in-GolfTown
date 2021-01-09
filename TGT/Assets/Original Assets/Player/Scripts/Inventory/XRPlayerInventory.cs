using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPlayerInventory : AbstractInventory
{
    public XRInteractions interactions;
    protected XRRayInteractor rayInteractor;

    protected bool clubGrabbed = false;

    // Button presses
    override protected void Start()
    {
        initInventory();

        rayInteractor = FindObjectOfType<XRRayInteractor>();
        interactions.leftPrimaryButtonPress.AddListener(pressed =>
        {
            if (pressed)
            {
                if (clubGrabbed)
                {
                    interactions.SendHapticImpulseToRightController(0.7f, 0.4f);
                    return;
                }

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
        });

        interactions.leftSecondaryButtonPress.AddListener(pressed =>
        {
            if (pressed)
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
        });

        interactions.leftTriggerButtonPress.AddListener(pressed =>
        {
            if (pressed)
            {
                if (raycasting)
                {
                    CancelRaycast(false);
                }

                tryPickupBall();
            }
        });

        XRDirectInteractor directInteractor = FindObjectOfType<XRDirectInteractor>();
        directInteractor.onSelectEntered.AddListener(interactable =>
        {
            clubGrabbed = true;
            interactions.SendHapticImpulseToRightController(0.5f, 0.2f);
            interactable.GetComponent<MeshCollider>().enabled = false;
        });

        directInteractor.onSelectExited.AddListener(interactable =>
        {
            Rigidbody rigidbody = interactable.GetComponent<Rigidbody>();
            if (rigidbody == null)
            {
                Debug.Log("Rigidbody is null");
            }
            else
            {
                rigidbody.useGravity = true;
            }
            clubGrabbed = false;
            interactable.GetComponent<MeshCollider>().enabled = true;
        });
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (raycasting)
        {
            RaycastBallHere();
        }
    }

    protected override bool doRaycast(out RaycastHit raycastHit, float interactionDistance = DEFAULT_INTERACTION_DISTANCE)
    {
        return rayInteractor.GetCurrentRaycastHit(out raycastHit);
    }

    // Clubs

    protected override void InstantiatePutter()
    {
        InstantiateClub(putterPrefab, transform.position + (transform.forward * 0.5f) + (transform.up * 1), transform.rotation);
    }

    protected override void InstantiateFiveIron()
    {
        InstantiateClub(fiveIronPrefab, transform.position + (transform.forward * 0.5f) + (transform.up * 1), transform.rotation);
    }
}
