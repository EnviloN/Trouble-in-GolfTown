using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public bool isSmallTalk;

    [TextArea(3, 10)]
    public string[] sentences; // things thay have to say

    public Dialogue(string name, string[] sentences) {
        this.isSmallTalk = false;
        this.sentences = sentences;
    }
}
