using UnityEngine;

public class Talkative : MonoBehaviour
{
    // Change radius where the player has to stay in order not to end dialogue.
    [Range(0.5f, 5f)]
    public float radius = 3f;

    public bool SmallTalk; // Random responses from the list.
    public Dialogue dialogue;

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
            if (SmallTalk)
            {
                // If small talk is turned on, a random sentence is chosen from dialogue and a new single-sentence dialogue is triggered
                int randomIndex = Random.Range(0, dialogue.sentences.Length);
                dm.StartDialogue(new Dialogue(dialogue.name, dialogue.sentences[randomIndex]));
            }
            else
            {
                dm.StartDialogue(dialogue); // Else full dialogue is started
            }
        }
    }

    // Ends dialogue
    public void EndDialogue()
    {
        isTalking = false;
        dm.EndDialogue();
    }
}
