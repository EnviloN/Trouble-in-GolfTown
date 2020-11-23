using System;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph {
    public string CharacterName;

    [NonSerialized]
    public GameStatus gameStatus;

    [NonSerialized]
    public DialogueNode Current; // The current "active" node

    public void UpdateCurrent() {
        Current.NextState();
    }
}
