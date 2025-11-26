using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionMenu : MonoBehaviour
{
    [Header("Token UI")]
    public TMP_Text upgradeTokenText;
    public TMP_Text rewindTokenText;

    [Header("Buttons")]
    public Button buyUpgradeButton;
    public Button buyMultipleUpgradeButton;
    public Button buyRewindButton;
    public Button buyMultipleRewindButton;
    public Button exitShopButton;

    [Header("Page Indexes")]
    public int losePageIndex = 4;

    private CombatManager cm;

    private void Start()
    {
        buyUpgradeButton.onClick.AddListener(OnBuyUpgrade);
        buyMultipleUpgradeButton.onClick.AddListener(OnMultipleBuyUpgrade);
        buyRewindButton.onClick.AddListener(OnBuyRewind);
        buyMultipleRewindButton.onClick.AddListener(OnMultipleBuyRewind);
        exitShopButton.onClick.AddListener(OnExitShop);
        cm = GameObject.FindAnyObjectByType<CombatManager>();
    }

    public void RefreshTokenText()
    {
        upgradeTokenText.text = ItemHandler.Instance.upgradeTokenCount.ToString("D4");
        rewindTokenText.text = ItemHandler.Instance.rewindTokenCount.ToString("D4");
    }

    private void OnBuyUpgrade()
    {
        ItemHandler.Instance.AddItem(ItemHandler.ItemType.UpgradeToken, 1);
        RefreshTokenText();
    }

    private void OnMultipleBuyUpgrade()
    {
        ItemHandler.Instance.AddItem(ItemHandler.ItemType.UpgradeToken, 2);
        RefreshTokenText();
    }

    private void OnBuyRewind()
    {
        ItemHandler.Instance.AddItem(ItemHandler.ItemType.RewindToken, 1);
        RefreshTokenText();
    }

    private void OnMultipleBuyRewind()
    {
        ItemHandler.Instance.AddItem(ItemHandler.ItemType.RewindToken, 2);
        RefreshTokenText();
    }

    private void OnExitShop()
    {
        UIManager.instance.GoToPage(losePageIndex);

        LoseMenu lose = FindAnyObjectByType<LoseMenu>();
        if (lose)
        {
            lose.Refresh();
        }
        cm.isInShop = false;
    }
}
