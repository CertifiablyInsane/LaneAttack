using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatIndicator : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private TextMeshProUGUI value;

    [Header("Icon Sprites")]
    [SerializeField] private Sprite healthIcon;
    [SerializeField] private Sprite damageIcon;
    [SerializeField] private Sprite speedIcon;
    [SerializeField] private Sprite healIntervalIcon;
    [SerializeField] private Sprite healDelayIcon;

    public void DisplayStat(Stat stat, float value)
    {
        icon.sprite = GetSpriteOfStat(stat);
        header.text = Enum.StatToString(stat);
        this.value.text = value.ToString();
    }

    private Sprite GetSpriteOfStat(Stat stat)
    {
        return stat switch
        {
            Stat.HEALTH => damageIcon,
            Stat.DAMAGE => damageIcon,
            Stat.SPEED => damageIcon,
            Stat.HEAL_INTERVAL => damageIcon,
            Stat.HEAL_DELAY => damageIcon,
            _ => throw new System.Exception()
        };
    }
}
