using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Outcome/StartBattle")]
public class StartBattleOutCome : DialogueOutcome
{
    //public string battleID;

    //public GameObject[] enemies;

    public override void Execute()
    {
        SceneController controller = GameObject.FindAnyObjectByType<SceneController>();
        controller.GoToCombatTestScene();   
    }
}
