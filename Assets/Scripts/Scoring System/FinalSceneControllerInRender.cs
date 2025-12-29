using UnityEngine;
using UnityEngine.UI;

public class FinalSceneControllerInRender : MonoBehaviour
{
    private SceneController sc;

    private Button mainMenu;

    private void Start()
    {
        sc = GameObject.FindAnyObjectByType<SceneController>();
        mainMenu = GetComponent<Button>();
        if (sc != null)
        {
            mainMenu.onClick.AddListener(() =>
            {
                sc.GoToMainMenu();
            });
        }
        else
            Debug.LogWarning("SC is null, player is trapped here.");
    }

}
