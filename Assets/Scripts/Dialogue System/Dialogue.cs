using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public string sentence;
    public DialogueChoice[] choices;

    public int nextLineIndex = -1;
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public int nextLineIndex; //Index to jump to in dialogue list
}
