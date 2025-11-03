using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Outcome/LoadDialogue")]
public class LoadDialogueOutcome : DialogueOutcome
{
    public TextAsset nextDialogueJSON;
    private Dialogue nextDialogue;
    private DialogueManager manager;

    public override void Execute()
    {
        manager = GameObject.FindAnyObjectByType<DialogueManager>();
        nextDialogue = manager.LoadDialogueFromJSON(nextDialogueJSON);
        manager.StartDialogue(nextDialogue);
    }
}
