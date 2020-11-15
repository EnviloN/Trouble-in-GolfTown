using System;
using UnityEngine;
using XNode;

[NodeWidth(500)]
public class DialogueNode : Node {
    [Input] public EmptyPort In;
    [Output] public EmptyPort Out;

    public Dialogue dialogue;

    public override object GetValue(NodePort port) {
        return null;
    }


    public void NextState() {
        DialogueGraph dialogueGraph = graph as DialogueGraph;

        if (dialogueGraph.Current != this) {
            Debug.LogWarning("Node isn't active");
            return;
        }

        NodePort exitPort = GetOutputPort("exit");

        if (!exitPort.IsConnected) {
            Debug.LogWarning("Node isn't connected");
            return;
        }

        DialogueNode node = exitPort.Connection.node as DialogueNode;
        node.EnterNode();
    }

    public void EnterNode() {
        DialogueGraph dialogueGraph = graph as DialogueGraph;
        dialogueGraph.Current = this;
    }

    [Serializable] public class EmptyPort { }
}