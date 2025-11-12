//Created by Wai
using UnityEngine;

public class PlayClickSound : MonoBehaviour
{
    public AudioSource clickSound; 

    public void PlaySound()
    {
        if (clickSound != null)
            clickSound.Play();
    }
}


