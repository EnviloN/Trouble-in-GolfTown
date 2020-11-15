using System;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph {
    public string CharacterName;

    [NonSerialized]
    public DialogueNode Current; // The current "active" node
}
