using UnityEngine;

public class MenuTransaction : MonoBehaviour
{
    [SerializeField] private Canvas TransactionCanvas;


    public void ShowCanvas()
    {
        if(TransactionCanvas != null)
            TransactionCanvas.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        if (TransactionCanvas != null)
            TransactionCanvas.gameObject.SetActive(false);
    }

}
