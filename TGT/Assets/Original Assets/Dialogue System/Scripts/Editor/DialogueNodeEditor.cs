using UnityEditor;
using UnityEngine;
using XNodeEditor;
using XNode;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : XNodeEditor.NodeEditor {
    public override void OnHeaderGUI() {
        GUI.color = Color.white;

        DialogueNode node = target as DialogueNode;
        DialogueGraph graph = node.graph as DialogueGraph;

        if (graph.Current == node)
            GUI.color = Color.green;
        string title = target.name;
        GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        GUI.color = Color.white;
    }

    public override void OnBodyGUI() {
        base.OnBodyGUI();
        DialogueNode node = target as DialogueNode;
        DialogueGraph graph = node.graph as DialogueGraph;

        if (GUILayout.Button("Set as current"))
            graph.Current = node;
    }
}
