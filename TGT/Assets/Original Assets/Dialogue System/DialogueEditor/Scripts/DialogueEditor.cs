using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditor : EditorWindow {
    private DialogueGraphView _graphView;

    [MenuItem("Graph/Dialogue Editor")]
    public static void OpenDialogueEditorWindow() {
        var window = GetWindow<DialogueEditor>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable() {
        ConstructGraphView();
        GenerateToolbar();
    }

    private void OnDisable() {
        rootVisualElement.Remove(_graphView);
    }

    private void ConstructGraphView() {
        _graphView = new DialogueGraphView {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar() {
        var toolbar = new Toolbar();

        var nodeCreationButton = new Button((() => {
            _graphView.CreateNode("Dialogue Node");
        }));
        nodeCreationButton.text = "New Node";
        toolbar.Add(nodeCreationButton);

        rootVisualElement.Add(toolbar);
    }
}
