using TMPro;
using UnityEngine;

public class UpgradesStation : MonoBehaviour
{
    [Header("Pointers")]
    [SerializeField] private AdvancedButton backButton;
    [SerializeField] private TextMeshProUGUI pointsDisplay;

    [Header("Player Upgrade Menu")]
    [SerializeField] private AdvancedButton upgradePlayerButton;
    [SerializeField] private PlayerStatsDisplay playerStatsDisplay;

    [Header("Unit Upgrade Menu")]
    [Header("Basic Unit")]
    [SerializeField] private AdvancedButton upgradeBasicUnitButton;
    [SerializeField] private BasicUnitStatsDisplay basicUnitStatsDisplay;


    private void Start()
    {
        backButton.AddOnClick(BackPressed);
        OnPointsChanged(GameManager.saveInfo.points);
        RefreshPlayerScreen(GameManager.saveInfo.playerLevel);
        RefreshUnitScreen(GameManager.saveInfo.basicUnitLevel);

        upgradePlayerButton.AddOnClick(UpgradePlayerButtonPressed);
        upgradeBasicUnitButton.AddOnClick(UpgradeBasicUnitButtonPressed);
    }
    private void OnEnable()
    {
        GameManager.OnPointsChanged += OnPointsChanged;
        GameManager.OnPlayerUpgraded += RefreshPlayerScreen;
        GameManager.OnBasicUnitUpgraded += RefreshUnitScreen;
    }
    private void OnDisable()
    {
        GameManager.OnPointsChanged -= OnPointsChanged;
        GameManager.OnPlayerUpgraded -= RefreshPlayerScreen;
        GameManager.OnBasicUnitUpgraded -= RefreshUnitScreen;
    }
    public void OnPointsChanged(int points)
    {
        pointsDisplay.text = "Points: " + points;

        bool canAffordPlayerUpgrade = points >= UpgradeData.PlayerUpgrades[GameManager.saveInfo.playerLevel].pointsForNextUpgrade;
        if (canAffordPlayerUpgrade)
            upgradePlayerButton.Enable();
        else upgradePlayerButton.Disable();

        bool canAffordBasicUnitUpgrade = points >= UpgradeData.BasicUnitUpgrades[GameManager.saveInfo.basicUnitLevel].pointsForNextUpgrade;
        if (canAffordBasicUnitUpgrade)
            upgradeBasicUnitButton.Enable();
        else upgradeBasicUnitButton.Disable();
        // TODO: The rest of the upgrade buttons
    }
    public void RefreshPlayerScreen(int playerLevel)
    {
        upgradePlayerButton.SetText("$ " + UpgradeData.PlayerUpgrades[playerLevel].pointsForNextUpgrade);
        playerStatsDisplay.SetUpgradeData(UpgradeData.PlayerUpgrades[playerLevel]);
    }
    public void RefreshUnitScreen(int basicUnitLevel)
    {
        upgradeBasicUnitButton.SetText("$ " + UpgradeData.BasicUnitUpgrades[basicUnitLevel].pointsForNextUpgrade);
        basicUnitStatsDisplay.SetUpgradeData(UpgradeData.BasicUnitUpgrades[basicUnitLevel]);
    }

    // Button Events
    public void UpgradePlayerButtonPressed(AdvancedButton _)
    {
        GameManager.AddPoints(-UpgradeData.PlayerUpgrades[GameManager.saveInfo.playerLevel].pointsForNextUpgrade);
        GameManager.Upgrade(Upgradable.PLAYER);
        GameManager.Save();
    }
    public void UpgradeBasicUnitButtonPressed(AdvancedButton _)
    {
        GameManager.AddPoints(-UpgradeData.BasicUnitUpgrades[GameManager.saveInfo.basicUnitLevel].pointsForNextUpgrade);
        GameManager.Upgrade(Upgradable.UNIT_BASICUNIT);
        GameManager.Save();
    }
    public void BackPressed(AdvancedButton _)
    {
        SceneLoader.Instance.LoadScene(Scene.LEVEL_SELECT);
    }
}
