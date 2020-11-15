using UnityEngine;
using XNode;

public class Talkative : MonoBehaviour
{
    // Change radius where the player has to stay in order not to end dialogue.
    [Range(0.5f, 5f)]
    public float radius = 3f;

    public string name; // character name
    public Dialogue dialogue;
    public NodeGraph dialogueGraph;

    private DummyPlayer player;
    private DialogueManager dm;

    private bool isTalking;
    private void Start()
    {
        player = FindObjectOfType<DummyPlayer>(); // Find reference to player
        dm = FindObjectOfType<DialogueManager>(); // Find reference to dialogue manager
        isTalking = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance > radius && isTalking)
        {
            EndDialogue();
        }
    }

    // In editor, display the radius with yellow sphere.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Triggers dialogue
    public void TriggerDialogue()
    {
        if (dm.IsDialogueActive())
        {
            dm.DisplayNextSentence(); // if a dialogue is already active, nest sentence is displayed
        }
        else
        {
            isTalking = true;
            dm.StartDialogue(name, dialogue);
        }
    }

    // Ends dialogue
    public void EndDialogue()
    {
        isTalking = false;
        dm.EndDialogue();
    }
}
