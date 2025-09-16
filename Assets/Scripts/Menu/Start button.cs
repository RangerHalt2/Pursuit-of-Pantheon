using UnityEngine;
using UnityEngine.SceneManagement; 

public class SwitchScene : MonoBehaviour
{
    
    public string scenename;

    public void LoadAssignedScene()
    {
        SceneManager.LoadScene(scenename);
    }
}
