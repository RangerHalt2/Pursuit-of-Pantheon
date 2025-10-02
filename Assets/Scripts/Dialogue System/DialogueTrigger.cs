using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset dialogueJSON;
    public DialogueManager manager;

    public void Start()
    {
        Dialogue dialogue = manager.LoadDialogueFromJSON(dialogueJSON);
        manager.StartDialogue(dialogue);
    }
}
