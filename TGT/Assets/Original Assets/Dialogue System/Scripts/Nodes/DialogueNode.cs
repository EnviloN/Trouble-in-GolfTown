using System;
using System.Reflection;
using UnityEngine;
using XNode;
using Object = System.Object;

[NodeWidth(500)]
public class DialogueNode : Node {
    [Input] public EmptyPort In;
    [Output(dynamicPortList = true)] public ConstraintPort[] Children;

    public Dialogue dialogue;

    public override object GetValue(NodePort port) {
        return null;
    }

    public void NextState() {
        DialogueGraph dialogueGraph = graph as DialogueGraph;

        if (dialogueGraph.Current != this) return;

        DialogueNode nextNode = null;
        for (int i = 0; i < Children.Length; i++) {
            if (Children[i].Evaluate(dialogueGraph)) {
                var port = GetOutputPort("Children " + i);
                if (port.IsConnected) {
                    nextNode = port.Connection.node as DialogueNode;
                    break;
                }
            }
        }
        if (nextNode != null)
            nextNode.EnterNode();
    }

    public void EnterNode() {
        DialogueGraph dialogueGraph = graph as DialogueGraph;
        dialogueGraph.Current = this;
    }

    [Serializable]
    public class EmptyPort { }


    [Serializable]
    public class ConstraintPort {
        public enum ConstraintType {
            IsTrue,
            IsGreaterThan
        }

        public string property;
        public ConstraintType type;
        public int number;

        public bool Evaluate(DialogueGraph graph) {
            switch (type) {
                case ConstraintType.IsTrue:
                    var boolValue = (bool) graph.gameStatus[property];
                    Debug.Log("Evalueating property " + property + " as Boolean. The property value is: " + boolValue);
                    return boolValue;
                case ConstraintType.IsGreaterThan:
                    var value = (int) graph.gameStatus[property];
                    Debug.Log("Evalueating property " + property + " as integer. The property value is: " + value);
                    return value > number;
                default:
                    return false;
            }
        }
    }
}
