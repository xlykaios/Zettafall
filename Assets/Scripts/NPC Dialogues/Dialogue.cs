using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    [TextArea(3, 10)]
    public string[] postFirstConvoSentences;

    [HideInInspector]
    public bool firstConvoOver = false;
}
