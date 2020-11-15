using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public KeyCode interactKey;
    public float interactionRayDistance = 2f;

    private DialogueManager dm;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Check if player is looking at talkative NPC
        dm.HideInteractability();
        if (Physics.Raycast(ray, out hit, interactionRayDistance))
        {
            Talkative talkative = hit.collider.GetComponent<Talkative>();
            if (talkative)
            {
                dm.DisplayInteractability();

                // if interact Key is pressed
                if (Input.GetKeyDown(interactKey))
                {
                    talkative.TriggerDialogue();
                }
            }

        }
    }
}
