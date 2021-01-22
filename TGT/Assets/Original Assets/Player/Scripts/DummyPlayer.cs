using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public KeyCode interactKey;
    public float interactionRayDistance = 2f;

    public Camera mainCamera;
    public XRInteractions interactions;

    private DialogueManager dm;
    private AbstractInventory inventory;

    private Talkative talkative;
    private SceneGate sceneGate;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
        inventory = FindObjectOfType<AbstractInventory>();
        interactions.rightTriggerButtonPress.AddListener(pressed => XRButtonPress(pressed));
        talkative = null;
        sceneGate = null;
    }

    // Update is called once per frame
    private void Update()
    {
        var ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // Check if player is looking at talkative NPC
        dm.HideInteractability();
        if (Physics.Raycast(ray, out var simpleHit, interactionRayDistance))
        {
            talkative = simpleHit.collider.GetComponent<Talkative>();
            if (talkative)
            {
                dm.DisplayInteractability(talkative);

                // if interact Key is pressed
                if (Input.GetKeyDown(interactKey))
                {
                    talkative.TriggerDialogue();
                }
            }

            sceneGate = simpleHit.collider.GetComponent<SceneGate>();
            if (sceneGate)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    sceneGate.LoadScene();
                }
            }
        }
    }

    private void XRButtonPress(bool pressed) {
        if (pressed) {
            if (talkative)
            {
                talkative.TriggerDialogue();
            }
            if (sceneGate)
            {
                sceneGate.LoadScene();
            }
        }
    }

    public void OnSceneChange()
    {
        inventory.RemoveClub();
        inventory.CancelRaycast();
    }
}
