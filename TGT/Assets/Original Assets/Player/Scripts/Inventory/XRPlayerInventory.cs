using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPlayerInventory : AbstractInventory
{
    public XRInteractions interactions;
    protected XRRayInteractor rayInteractor;

    public GameObject ballCountCanvasPrefab;
    protected GameObject ballCountCanvasObject = null;

    protected bool clubGrabbed = false;

    // Button presses
    override protected void Start()
    {
        initInventory();

        rayInteractor = FindObjectOfType<XRRayInteractor>();
        interactions.rightPrimaryButtonPress.AddListener(pressed =>
        {
            if (pressed)
            {
                // If player has club in hand, they cannot change it (results in Null exception)
                if (clubGrabbed)
                {
                    interactions.SendHapticImpulseToRightController(0.7f, 0.4f);
                    return;
                }

                if (!havePutterClub && !have5IronClub)
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
                        // Destroy(clubObject.gameObject); - generates wierd error about MeshCollider not found
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
                        // Destroy(clubObject.gameObject); - generates wierd error about MeshCollider not found
                        HideClub();
                        clubInHandState = 0;
                        break;
                }
            }
        });

        interactions.leftPrimaryButtonPress.AddListener(pressed =>
        {
            if (pressed)
            {
                if (raycasting)
                {
                    CancelRaycast(true);
                }
                else if (CanPlaceBallHere())
                {
                    if (haveBall())
                    {
                        raycasting = true;
                    } else
                    {
                        interactions.SendHapticImpulseToLeftController(0.7f, 0.4f);
                    }
                }
            }
        });

        interactions.leftTriggerButtonPress.AddListener(pressed =>
        {
            if (pressed)
            {
                tryPickupBall();
            }
        });

        XRDirectInteractor directInteractor = FindObjectOfType<XRDirectInteractor>();
        directInteractor.onSelectEntered.AddListener(interactable =>
        {
            clubGrabbed = true;
            interactions.SendHapticImpulseToRightController(0.5f, 0.2f);
            MeshCollider collider = interactable.GetComponent<MeshCollider>();
            if (collider != null) {
                collider.enabled = false;
            }
        });

        directInteractor.onSelectExited.AddListener(interactable =>
        {
            if (interactable == null) {
                return;
            }
            Rigidbody rigidbody = interactable.GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            clubGrabbed = false;
            MeshCollider collider = interactable.GetComponent<MeshCollider>();
            if (collider != null) {
                collider.enabled = true;
            }
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

    protected override void RaycastBallHere()
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
                ShowBallCounter(raycastHit.point);
            }
        }
    }

    public override void CancelRaycast(bool addBallBackToInventory = true)
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
            HideBallCounter();
            ballObject = null;
        }
    }

    // Clubs
    protected override void InstantiatePutter()
    {
        InstantiateClub(putterPrefab, ref putterObject, transform.position + (transform.forward * 0.5f) + (transform.up * 1), transform.rotation);
    }

    protected override void InstantiateFiveIron()
    {
        InstantiateClub(fiveIronPrefab, ref fiveIronObject, transform.position + (transform.forward * 0.5f) + (transform.up * 1), transform.rotation);
    }

    #region Ball counter canvas methods

    protected void ShowBallCounter(Vector3 position)
    {
        Vector3 relativePosFromBall = (transform.forward * 0.1f) + (transform.up * 0.2f);

        if (ballCountCanvasObject == null)
        {
            ballCountCanvasObject = Instantiate(ballCountCanvasPrefab, position + relativePosFromBall, Quaternion.LookRotation(transform.position - position));
            setCountOfBallsOnCanvas();
        }
        else
        {
            ballCountCanvasObject.transform.SetPositionAndRotation(position + relativePosFromBall, ballCountCanvasObject.transform.rotation);
        }
    }

    protected void HideBallCounter()
    {
        if (ballCountCanvasObject != null)
        {
            Destroy(ballCountCanvasObject);
        }
    }

    protected void setCountOfBallsOnCanvas()
    {
        if (ballCountCanvasObject != null)
        {
            string textBuilder = "You have " + numOfBalls + " ball";
            if (numOfBalls > 1)
            {
                textBuilder += "s";
            }
            ballCountCanvasObject.GetComponentInChildren<Text>().text = textBuilder;
        }
    }

    #endregion
}
