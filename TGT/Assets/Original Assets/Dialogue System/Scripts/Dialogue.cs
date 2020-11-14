using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name; // character name

    [TextArea(3, 10)]
    public string[] sentences; // things thay have to say

    public Dialogue(string name, string[] sentences)
    {
        this.name = name;
        this.sentences = sentences;
    }

    public Dialogue(string name, string sentence)
    {
        this.name = name;
        this.sentences = new string[] { sentence };
    }
}
