using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset dialogueJSON;
    public DialogueManager manager;

    private bool dialogueStarted = false;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    TriggerDialogue();
                }
            }
        }
    }

    public void TriggerDialogue()
    {
        if (dialogueStarted) return;
        dialogueStarted = true;

        if (manager == null || dialogueJSON == null)
        {
            Debug.LogWarning("DialogueManager or dialogueJSON is not assigned");
            return;
        }

        Dialogue dialogue = manager.LoadDialogueFromJSON(dialogueJSON);
        manager.StartDialogue(dialogue);
    }
}

/*for future on calling this file use:
 
 public DialogueTrigger npcDialogueTrigger;

 void OnBattleWin()
{
    npcDialogueTrigger.TriggerDialogue();
}

 this script will be attached to NPC GameObjects that have colliders on them (set to trigger)*/
