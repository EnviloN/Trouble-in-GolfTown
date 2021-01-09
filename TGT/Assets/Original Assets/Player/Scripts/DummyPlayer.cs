using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public KeyCode interactKey;
    public float interactionRayDistance = 2f;

    public Camera mainCamera;
    public XRInteractions interactions;
    private bool rightPrimaryButtonIsPressed = false;

    private DialogueManager dm;
    private AbstractInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
        inventory = FindObjectOfType<AbstractInventory>();
        interactions.rightPrimaryButtonPress.AddListener(pressed => rightPrimaryButtonIsPressed = pressed);
    }

    // Update is called once per frame
    private void Update()
    {
        var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // Check if player is looking at talkative NPC
        dm.HideInteractability();
        if (Physics.Raycast(ray, out var simpleHit, interactionRayDistance))
        {
            var talkative = simpleHit.collider.GetComponent<Talkative>();
            if (talkative)
            {
                dm.DisplayInteractability();

                // if interact Key is pressed
                if (Input.GetKeyDown(interactKey) || rightPrimaryButtonIsPressed)
                {
                    talkative.TriggerDialogue();
                }
            }

            var gate = simpleHit.collider.GetComponent<SceneGate>();
            if (gate)
            {
                if (Input.GetKeyDown(interactKey) || rightPrimaryButtonIsPressed)
                {
                    gate.LoadScene();
                }
            }
        }

        // if interact Key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            dm.UpdateGraphs();
        }
    }

    public void OnSceneChange()
    {
        inventory.RemoveClub();
        inventory.CancelRaycast();
    }
}
