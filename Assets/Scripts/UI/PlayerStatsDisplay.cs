using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healDelayText;
    [SerializeField] private TextMeshProUGUI healIntervalText;
    [SerializeField] private TextMeshProUGUI speedText;

    public void SetUpgradeData(PlayerUpgrade data)
    {
        healthText.text = data.health.ToString();
        damageText.text = data.damage.ToString();
        healDelayText.text = data.healDelay.ToString();
        healIntervalText.text = data.healInterval.ToString();
        speedText.text = data.speed.ToString();
    }
}
