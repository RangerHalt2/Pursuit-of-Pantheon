using UnityEngine;
using UnityEngine.UI;

public class ButtonSwap : MonoBehaviour
{

    [Header("Buttons")]
    public bool isOpen;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;


    private void Start()
    {
        openButton.gameObject.SetActive(!isOpen);
        closeButton.gameObject.SetActive(isOpen);
    }

    public void ToggleButton()
    {
        isOpen = !isOpen;
        openButton.gameObject.SetActive(!isOpen);
        closeButton.gameObject.SetActive(isOpen);
    }

    public void SetClosed()
    {
        isOpen = false;
        closeButton.gameObject.SetActive(isOpen);
        openButton.gameObject.SetActive(!isOpen);
    }
}
