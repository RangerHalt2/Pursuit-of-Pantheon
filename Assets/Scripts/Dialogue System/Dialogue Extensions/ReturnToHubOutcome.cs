using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Outcome/ReturnToHub")]
public class ReturnToHubOutcome : DialogueOutcome
{
    private SceneController sceneController;

    public override void Execute()
    {
        sceneController = GameObject.FindAnyObjectByType<SceneController>();
        Debug.Log("Returning to hub...");
        if (sceneController != null)
            sceneController.GoToScene("HubWorld");
    }
}
