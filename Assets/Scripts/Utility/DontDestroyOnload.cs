// Created By Wai
using UnityEngine;

public class PersistentAsset : MonoBehaviour
{
    [Header("Dead here to keep onload)")]
    public GameObject assetToKeep;

    private void Awake()
    {
        
        if (assetToKeep != null && assetToKeep.scene.name == null)
        {
            assetToKeep = Instantiate(assetToKeep);
        }

       
        DontDestroyOnLoad(assetToKeep);
        DontDestroyOnLoad(gameObject);
    }
}
