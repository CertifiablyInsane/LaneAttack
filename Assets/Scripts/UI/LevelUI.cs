using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : Singleton<LevelUI>
{
    [Header("Data UI")]
    [SerializeField] private TextMeshProUGUI moneyUI;
    [SerializeField] private TextMeshProUGUI levelNameUI;

    [Header("Unit Button")]
    [SerializeField] private Transform unitButtonContainer;
    [SerializeField] private UnitButton unitButtonPrefab;

    [Header("Overlay Screens")]
    [SerializeField] private ScreenToggle overlayScreenToggle;
    [SerializeField] private AdvancedButton failRestartButton;
    [SerializeField] private AdvancedButton failLevelSelectButton;
    [SerializeField] private AdvancedButton failUpgradesButton;
    [SerializeField] private AdvancedButton completeContinueButton;
    [SerializeField] private AdvancedButton completeUpgradesButton;
    [SerializeField] private AdvancedButton pauseMenuButton;
    [SerializeField] private AdvancedButton pauseRestartButton;
    [SerializeField] private AdvancedButton pauseLevelSelectButton;
    [SerializeField] private AdvancedButton pauseResumeButton;

    private void Start()
    {
        levelNameUI.text = "Level " + GameManager.currentLevel.levelName;
        failRestartButton.AddOnClick(RestartButtonPress);           // Fail Menu => Restart Level
        failLevelSelectButton.AddOnClick(LevelSelectButtonPress);   // Fail Menu => Level Select
        failUpgradesButton.AddOnClick(UpgradesButtonPress);         // Fail Menu => Upgrades Station
        completeContinueButton.AddOnClick(LevelSelectButtonPress);  // Complete Menu => Level Select
        completeUpgradesButton.AddOnClick(UpgradesButtonPress);     // Complete Menu => Upgrades Station
        pauseMenuButton.AddOnClick(PauseMenuButtonPress);           // Pause Menu => Open Menu
        pauseRestartButton.AddOnClick(RestartButtonPress);          // Pause Menu => Restart Level
        pauseLevelSelectButton.AddOnClick(LevelSelectButtonPress);  // Pause Menu => Level Select
        pauseResumeButton.AddOnClick(ResumeButtonPress);            // Pause Menu => Close Menu
    }
    private void OnEnable()
    {
        LevelManager.OnMoneyChanged += OnMoneyChanged;
    }
    private void OnDisable()
    {
        LevelManager.OnMoneyChanged -= OnMoneyChanged;
    }
    private void OnMoneyChanged(int amount)
    {
        moneyUI.text = amount.ToString();
    }
    public void GenerateUnitButtons(UnitData[] units)
    {
        foreach (UnitData unit in units)
        {
            UnitButton ub = Instantiate(unitButtonPrefab, unitButtonContainer);
            ub.SetData(unit);
        }
    }

    // BUTTON FUNCTIONS
    public void RestartButtonPress(AdvancedButton _)
    {
        SceneLoader.Instance.ReloadScene();
    }
    public void LevelSelectButtonPress(AdvancedButton _)
    {
        SceneLoader.Instance.LoadScene("Level Select");
    }
    public void PauseMenuButtonPress(AdvancedButton _)
    {
        overlayScreenToggle.SetScreen(2);
        GameManager.Pause();
    }
    public void ResumeButtonPress(AdvancedButton _)
    {
        GameManager.UnPause();
        overlayScreenToggle.DisableAll();
    }
    public void UpgradesButtonPress(AdvancedButton _)
    {
        // SceneLoader.Instance.LoadScene("Upgrades")
        Debug.LogWarning("Upgrades Menu Not Implemented! Will not load scene.");
    }
}
