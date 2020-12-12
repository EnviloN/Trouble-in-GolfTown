using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public KeyCode interactKey;
    public float interactionRayDistance = 2f;

    public XRInteractions interactions;
    private bool primaryButtonIsPressed = false;

    private DialogueManager dm;
    private Inventory inventory;
    private GameStatus gameStatus;
    private GameManager GM;
    private GiantGolfBall giantBall;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
        inventory = FindObjectOfType<Inventory>();
        gameStatus = FindObjectOfType<GameStatus>();
        GM = FindObjectOfType<GameManager>();
        giantBall = FindObjectOfType<GiantGolfBall>();
        interactions.primaryButtonPress.AddListener(onPrimaryButtonEvent);
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        primaryButtonIsPressed = pressed;
    }

    // Update is called once per frame
    private void Update()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // Check if player is looking at talkative NPC
        dm.HideInteractability();
        if (Physics.Raycast(ray, out var simpleHit, interactionRayDistance)) {
            var talkative = simpleHit.collider.GetComponent<Talkative>();
            if (talkative) {
                dm.DisplayInteractability();

                // if interact Key is pressed
                if (Input.GetKeyDown(interactKey) || primaryButtonIsPressed) {
                    talkative.TriggerDialogue();
                }
            }

            var gate = simpleHit.collider.GetComponent<SceneGate>();
            if (gate) {
                if (Input.GetKeyDown(interactKey) || primaryButtonIsPressed) {
                    gate.LoadScene();
                }
            }
        }

        // if interact Key is pressed
        if (Input.GetKeyDown(KeyCode.R)) {
            dm.UpdateGraphs();
        }

        if (Input.GetKeyDown(interactKey)) {
            var hits = Physics.RaycastAll(ray, interactionRayDistance);
            foreach (var hit in hits) {
                Placeholder ball = hit.collider.GetComponent<Placeholder>();
                GoldenBallPlaceholder goldenBall = hit.collider.GetComponent<GoldenBallPlaceholder>();

                if (!ball && !goldenBall) continue;
                
                if (ball) {
                    ball.gameObject.SetActive(false);
                    inventory.addBall();
                    var count = (int) GM.GetGameStatus("ballsCollected") + 1;
                    GM.SetGameStatus("ballsCollected", count);
                    break;
                }
                if (goldenBall) {
                    goldenBall.gameObject.SetActive(false);
                    inventory.addBall();
                    GM.AddGoldenBall();
                    if (GM.HaveCollectedAllGoldenBalls())
                    {
                        giantBall.GetComponent<MeshRenderer>().enabled = true;
                        giantBall.GetComponent<MeshCollider>().enabled = true;
                    }
                    break;
                }
            }
        }
    }
}
