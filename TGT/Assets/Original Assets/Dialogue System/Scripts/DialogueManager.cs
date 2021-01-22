using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour
{
    // References to the UI text elements (so we can change the texts on the screen)
    public Text nameText;
    public Text dialogueText;

    // Animator handling animations of UI
    public Animator DialogBoxAnimator;
    public Animator TalkIconAnimator;

    public GameStatus gameStatus;
    public GameManager GM;

    private Queue<string> sentences; // Queue of sentences in active dialogue
    private string currentCharacterName; 
    private bool dialogueActive; // flag if dualogue is ongoing

    private Dictionary<string, string> currentDialogueNodes;

    void Awake()
    {
        // initialize
        sentences = new Queue<string>();
        currentDialogueNodes = new Dictionary<string, string>();
        dialogueActive = false;
    }

    // Starts a given dialog
    public void StartDialogue(string characterName, Dialogue dialogue)
    {
        HideInteractability();
        DialogBoxAnimator.SetBool("isOpen", true);

        // Fill the queue
        if (dialogue.isSmallTalk) {
            // If small talk is turned on, a random sentence is chosen from dialogue
            int randomIndex = Random.Range(0, dialogue.sentences.Length);
            sentences.Enqueue(dialogue.sentences[randomIndex]);
        } else {
            foreach (string sentence in dialogue.sentences) {
                sentences.Enqueue(sentence);
            }
        }

        // Start the dialogue
        if (sentences.Count != 0)
        {
            dialogueActive = true;
            currentCharacterName = characterName;
            DisplayNextSentence();
        }
    }

    // Displays next sentence from the queue if available. If not, dialogue ends.
    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string raw_sentence = sentences.Dequeue();
        if (raw_sentence.StartsWith("[player]")) {
            nameText.text = "Player";
            dialogueText.text = raw_sentence.Remove(0, 8).Trim();
        } else if (raw_sentence.StartsWith("[status]")) {
            var statusSet = raw_sentence.Remove(0, 8).Trim();
            EndDialogue();

            foreach (var status in statusSet.Split('|')) {
                var property = status.Split('=')[0];
                var value = Convert.ToInt32(status.Split('=')[1]);
                GM.SetGameStatus(property, value);
            }
            return;
        } else if (raw_sentence.StartsWith("[incStatus]")) {
        var statusSet = raw_sentence.Remove(0, 11).Trim();
        EndDialogue();

        foreach (var property in statusSet.Split('|')) {
            var value = (int) GM.GetGameStatus(property) + 1;
            GM.SetGameStatus(property, value);
        }
        return;
    } else {
            nameText.text = currentCharacterName;
            dialogueText.text = raw_sentence;
        }
    }

    // Ends the current dialog
    public void EndDialogue()
    {
        dialogueActive = false;
        DialogBoxAnimator.SetBool("isOpen", false);
        sentences.Clear(); // Clean all remaining sentences
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

    public void UpdateGraphs() {
        var talkatives = FindObjectsOfType<Talkative>();
        foreach (var talkative in talkatives) {
            talkative.dialogueGraph.UpdateCurrent();
            updateCurrentGUID(talkative.dialogueGraph.CharacterName, talkative.dialogueGraph.Current.GUID);
        }
    }

    public string getOrInsertCurrentGUID(string name, string dialogueGUID) {
        if (!currentDialogueNodes.ContainsKey(name)) {
            currentDialogueNodes.Add(name, dialogueGUID);
        }

        return currentDialogueNodes[name];
    }

    public void updateCurrentGUID(string name, string dialogueGUID) {
        currentDialogueNodes[name] = dialogueGUID;
    }

    public void RetreivePersistantStatuses() {
        var talkatives = FindObjectsOfType<Talkative>();
        foreach (var talkative in talkatives) {
            talkative.RetrieveStatus();
        }
    }
}
