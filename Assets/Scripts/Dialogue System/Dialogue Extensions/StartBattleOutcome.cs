using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Outcome/StartBattle")]
public class StartBattleOutCome : DialogueOutcome
{
    //public string battleID;

    //public GameObject[] enemies;

    public bool hasStoryAfterBattle = false;

    public TextAsset nextDialogueJSON;
    private Bootstrapper boot;

    public List<EnemySpawnData> enemiesToSpawn = new List<EnemySpawnData>();

    public override void Execute()
    {
        boot = GameObject.FindAnyObjectByType<Bootstrapper>();

        boot.combatLeadsToDialogue = hasStoryAfterBattle;
        boot.dialogueAfterBattle = nextDialogueJSON;

        boot.enemiesForNextBattle = enemiesToSpawn;

        SceneController controller = GameObject.FindAnyObjectByType<SceneController>();
        controller.GoToCombatTestScene();
    }
}
