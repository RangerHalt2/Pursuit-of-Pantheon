using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyAllOnMainMenu : MonoBehaviour
{

    public static void Clear()
    {
        // Create a temp object to locate DDOL scene
        GameObject temp = new GameObject("DDOL_TEMP");
        Object.DontDestroyOnLoad(temp);

        var ddolScene = temp.scene;

        // Get all objects in the DDOL scene
        var roots = ddolScene.GetRootGameObjects();

        foreach (var obj in roots)
        {
            if (obj != temp) // delete everything *except* the temp reference
                Object.Destroy(obj);
        }

        // Now delete the temp object too
        Object.Destroy(temp);

        Debug.Log("Deleted all DontDestroyOnLoad objects.");
    }

}
