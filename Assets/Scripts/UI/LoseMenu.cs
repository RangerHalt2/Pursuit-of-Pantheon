using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button rewindButton;
    public Button backToHubButton;

    [Header("References")]
    public int shopPageIndex = 5;
    public int losePageIndex = 4;

    private void Start()
    {
        rewindButton.onClick.AddListener(OnRewindPressed);
        backToHubButton.onClick.AddListener(OnBackToHubPressed);
    }

    public void OnRewindPressed()
    {
        Debug.Log("Rewind pressed");
        bool hasToken = ItemHandler.Instance.RemoveItem(ItemHandler.ItemType.RewindToken, 1);

        if (hasToken)
        {
            Time.timeScale = 1f;
            CombatManager.instance.RestartBattle();
        }
        else
        {
            int shopPageIndex = 5;
            Debug.Log("No Rewind Token. Opening shop page");

            UIManager.instance.GoToPage(shopPageIndex);

            if (UIManager.instance.pages.Count > shopPageIndex)
            {
                UIPage shopPage = UIManager.instance.pages[shopPageIndex];
                if (shopPage != null)
                {
                    TransactionMenu shopMenu = shopPage.GetComponent<TransactionMenu>();
                    if (shopMenu != null)
                    {
                        shopMenu.RefreshTokenText();
                    }
                    else
                    {
                        Debug.LogWarning("TransactionMenu component not found " + shopPageIndex);
                    }
                }
                else
                {
                    Debug.LogWarning("UIManager pages list does not contain index " + shopPageIndex);
                }

                Time.timeScale = 1f;
            }
        }
    }

    public void Refresh()
    {
        TransactionMenu shop = FindAnyObjectByType<TransactionMenu>();
        if (shop)
        {
            shop.RefreshTokenText();
        }
    }

    private void OnBackToHubPressed()
    {
        Time.timeScale = 1f;
        SceneController controller = FindAnyObjectByType<SceneController>();
        controller.GoToScene("HubWorld");
    }
}
