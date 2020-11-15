using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView {
    private readonly Vector2 defaultNodeSize = new Vector2(150, 200);

    public DialogueGraphView() {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var gridBG = new GridBackground();
        Insert(0, gridBG);
        gridBG.StretchToParentSize();

        AddElement(GenerateStartNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
        var compatiblePorts = new List<Port>();

        ports.ForEach((port) => {
            if (startPort != port && startPort.node != port.node) {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }

    private DialogueNode GenerateStartNode() {
        var node = new DialogueNode {
            title = "START", GUID = Guid.NewGuid().ToString(), Dialogue = new Dialogue(), EntryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 100, 100, 150));
        return node;
    }

    private Port GeneratePort(DialogueNode node, Direction portdDirection,
        Port.Capacity capacity = Port.Capacity.Single) {
        return node.InstantiatePort(Orientation.Horizontal, portdDirection, capacity, typeof(float));
    }

    public void CreateNode(string nodeName) {
        AddElement(CreateDialogueNode(nodeName));
    }

    public DialogueNode CreateDialogueNode(string nodeName) {
        var dialogueNode = new DialogueNode {
            GUID = Guid.NewGuid().ToString(), title = nodeName, Dialogue = new Dialogue()
        };

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        var button = new Button((() => {
            AddChoicePort(dialogueNode);
        }));
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return dialogueNode;
    }

    private void AddChoicePort(DialogueNode dialogueNode) {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;
        generatedPort.portName = $"Choice {outputPortCount}";

        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();

    }
}
