using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Outcome/StartBattle")]
public class StartBattleOutCome : DialogueOutcome
{
    //public string battleID;

    //public GameObject[] enemies;

    public bool hasStoryAfterBattle = false;

    public TextAsset nextDialogueJSON;
    private Bootstrapper boot;

    public override void Execute()
    {
        boot = GameObject.FindAnyObjectByType<Bootstrapper>();
        boot.combatLeadsToDialogue = hasStoryAfterBattle;
        boot.dialogueAfterBattle = nextDialogueJSON;
        SceneController controller = GameObject.FindAnyObjectByType<SceneController>();
        controller.GoToCombatTestScene();
    }
}
