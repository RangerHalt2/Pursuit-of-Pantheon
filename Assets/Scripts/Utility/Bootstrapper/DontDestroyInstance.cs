//Any object that should not be destroyed can be found here.

using UnityEngine;

public class DontDestroyInstance : MonoBehaviour
{
    private static DontDestroyInstance instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
