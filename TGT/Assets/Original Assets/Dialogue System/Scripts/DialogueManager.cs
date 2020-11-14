using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // References to the UI text elements (so we can change the texts on the screen)
    public Text nameText;
    public Text dialogueText;

    // Animator handling animations of UI
    public Animator DialogBoxAnimator;
    public Animator TalkIconAnimator;

    private Queue<string> sentences; // Queue of sentences in active dialogue
    private bool dialogueActive; // flag if dualogue is ongoing

    void Start()
    {
        // initialize
        sentences = new Queue<string>();
        dialogueActive = false;
    }

    // Starts a given dialog
    public void StartDialogue(Dialogue dialogue)
    {
        HideInteractability();
        DialogBoxAnimator.SetBool("isOpen", true);

        // Fill the queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Start the dialogue
        if (sentences.Count != 0)
        {
            dialogueActive = true;
            nameText.text = dialogue.name;
            DisplayNextSentence();
        }
    }

    // Displays next sentence from the queue if available. If not, dialogue ends.
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    // Ends the current dialog
    public void EndDialogue()
    {
        dialogueActive = false;
        DialogBoxAnimator.SetBool("isOpen", false);
        sentences.Clear(); // Clean all remaining sentences
        Debug.Log("Conversation ended.");
    }

    public bool IsDialogueActive() { return dialogueActive; }

    public void DisplayInteractability()
    {
        if (!dialogueActive)
        {
            TalkIconAnimator.SetBool("isVisible", true);
        }
    }

    public void HideInteractability()
    {
        TalkIconAnimator.SetBool("isVisible", false);
    }
}
