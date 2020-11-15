using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogueNode : Node {
    public string GUID;

    public Dialogue Dialogue;
    public bool EntryPoint = false;
}
