using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class DialogueLoader : MonoBehaviour
{
    public async Task<TextAsset> LoadDialogueJSON(string address)
    {
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(address);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogWarning($"Failed to load dialogue JSON at address {address}");
            return null;
        }
    }
}