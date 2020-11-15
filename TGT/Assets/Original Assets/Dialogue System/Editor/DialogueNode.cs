using UnityEngine;
using XNode;

public class DialogueNode : Node {
    [Input] public float a;
    [Output] public float b;

    public Dialogue dialogue;

    public override object GetValue(NodePort port) {
        if (port.fieldName == "b")
            return GetInputValue<float>("a", a);
        else
            return null;
    }
}