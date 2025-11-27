using System.Collections;
using TMPro;
using UnityEngine;

public class TimedSceneController : MonoBehaviour
{
    SceneController sceneController;
    private void Start()
    {
        sceneController = GetComponent<SceneController>();
        if (sceneController != null)
            StartCoroutine(GoToMainMenu());
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(3f);

        sceneController.GoToScene("MainMenu");

    }
}
