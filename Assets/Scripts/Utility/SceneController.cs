using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Dynamic Scene Naming capabilities
    [SerializeField] private string MainMenu = "MainMenu";

    void Start()
    {
        MainMenu = "MainMenu";
    }

    // Closes out of the application
    public void Quit()
    {
        Application.Quit();
    }

    public void GoToScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        DestroyAllOnMainMenu.Clear();
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }

    //Hard Coded Scene Management for testing scene
    public void GoToCombatTestScene()
    {
        SceneManager.LoadScene("CombatTestScene");
    }

}
