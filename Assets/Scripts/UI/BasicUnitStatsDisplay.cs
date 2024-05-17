using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicUnitStatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;

    public void SetUpgradeData(BasicUnitUpgrade data)
    {
        healthText.text = data.health.ToString();
        damageText.text = data.damage.ToString();
        speedText.text = data.speed.ToString();
    }
}
