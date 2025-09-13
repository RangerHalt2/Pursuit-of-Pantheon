// Created By: Ryan Lupoli
// This script is intended for overall game management, will be fleshed out far more at a later date
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 
    // Global instance which other scripts can reference
    public static GameManager instance = null;

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Ensure there is only one instance of the Game Manager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // Restarts the current scene (used for respawning)
    public void RestartCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

}
