using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{

    [SerializeField] private StatIndicator indicatorPrefab;

    [Header("Pointers")]
    [SerializeField] private AdvancedButton upgradeButton;
    [SerializeField] private Transform statIndicatorContainer;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI upgradeButtonText;

    private readonly List<StatIndicator> _indicators = new();
    private ScriptableObject _currentDataDisplayed;

    private void Start()
    {
        upgradeButton.AddOnClick(UpgradeButtonPressed);
    }

    public void DisplayData(UnitData unit)
    {
        _currentDataDisplayed = unit;   // Store the current thing being displayed
        Upgrade[] upgradeArray = UpgradeData.GetUpgradeArrayFromUnitData(unit);
        Upgrade thisLevel = upgradeArray[GameManager.GetLevelOfUnit(unit)];

        // Enable or disable the upgrade button
        if (GameManager.SaveInfo.points < thisLevel.pointsForNextUpgrade)
            upgradeButton.Disable();    // We do not have the funds to buy this
        else
            upgradeButton.Enable();     // We have the funds to buy this

        // Update the name and icon
        nameText.text = unit.displayName;
        icon.sprite = unit.icon;

        GenerateIndicators(thisLevel);
    }

    public void DisplayData(ProtagonistData protag)
    {
        _currentDataDisplayed = protag; // Store the current thing being displayed
        Upgrade[] upgradeArray = UpgradeData.GetUpgradeArrayFromProtagonistData(protag);
        Upgrade thisLevel = upgradeArray[GameManager.GetLevelOfProtagonist(protag)];


        // Update name and icon
        nameText.text = protag.displayName;
        icon.sprite = protag.icon;

        GenerateIndicators(thisLevel);
    }

    private void GenerateIndicators(Upgrade upgr)
    {
        // Update button price
        upgradeButtonText.text = $"Upgrade to Level {upgr.level + 1}: ${upgr.pointsForNextUpgrade}";

        // Purge the old indicators
        foreach (var indicator in _indicators)
        {
            Destroy(indicator.gameObject);
        }
        _indicators.Clear();

        // HEALTH
        if (upgr.health != 0)
        {
            StatIndicator si = Instantiate(indicatorPrefab, statIndicatorContainer);
            si.DisplayStat(Stat.HEALTH, upgr.health);
            _indicators.Add(si);
        }

        // DAMAGE
        if (upgr.damage != 0)
        {
            StatIndicator si = Instantiate(indicatorPrefab, statIndicatorContainer);
            si.DisplayStat(Stat.DAMAGE, upgr.damage);
            _indicators.Add(si);
        }

        // SPEED
        if (upgr.speed != 0)
        {
            StatIndicator si = Instantiate(indicatorPrefab, statIndicatorContainer);
            si.DisplayStat(Stat.SPEED, upgr.speed);
            _indicators.Add(si);
        }

        // HEAL DELAY
        if (upgr.healDelay != 0)
        {
            StatIndicator si = Instantiate(indicatorPrefab, statIndicatorContainer);
            si.DisplayStat(Stat.HEAL_DELAY, upgr.healDelay);
            _indicators.Add(si);
        }

        // HEAL INTERVAL
        if (upgr.healInterval != 0)
        {
            StatIndicator si = Instantiate(indicatorPrefab, statIndicatorContainer);
            si.DisplayStat(Stat.HEAL_INTERVAL, upgr.healInterval);
            _indicators.Add(si);
        }
    }
    public void ReloadDisplay()
    {
        if (_currentDataDisplayed is UnitData ud)
            DisplayData(ud);
        else if (_currentDataDisplayed is ProtagonistData pd)
            DisplayData(pd);
    }
    public void UpgradeButtonPressed(AdvancedButton _)
    {
        if(_currentDataDisplayed is UnitData unitData) 
        {
            Upgrade[] upgradeArray = UpgradeData.GetUpgradeArrayFromUnitData(unitData);
            Upgrade thisLevel = upgradeArray[GameManager.GetLevelOfUnit(unitData)];

            if (GameManager.SaveInfo.points < thisLevel.pointsForNextUpgrade)
            {
                Debug.LogError("Somehow pressed the upgrade button without having the funds to do so. Exiting function!");
                return;
            }
            GameManager.AddPoints(-thisLevel.pointsForNextUpgrade);
            GameManager.Upgrade(unitData);
        }
        else if (_currentDataDisplayed is ProtagonistData protagData)
        {
            Upgrade[] upgradeArray = UpgradeData.GetUpgradeArrayFromProtagonistData(protagData);
            Upgrade thisLevel = upgradeArray[GameManager.GetLevelOfProtagonist(protagData)];

            if (GameManager.SaveInfo.points < thisLevel.pointsForNextUpgrade)
            {
                Debug.LogError("Somehow pressed the upgrade button without having the funds to do so. Exiting function!");
                return;
            }
            GameManager.AddPoints(-thisLevel.pointsForNextUpgrade);
            GameManager.Upgrade(protagData);
        }
        GameManager.Save();
        ReloadDisplay();
    }
}
