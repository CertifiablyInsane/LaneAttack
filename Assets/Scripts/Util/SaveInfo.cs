using System;
using UnityEngine;

[Serializable]
public class SaveInfo
{
    public string username;
    public int level;
    public int points;

    // Unit Unlock Status
    public bool robotBasicUnitUnlocked;
    public bool robotSwarmUnitUnlocked;
    public bool robotRangedUnitUnlocked;
    public bool robotKnockbackUnitUnlocked;
    public bool robotSupportUnitUnlocked;

    public bool martianBasicUnitUnlocked;
    public bool martianHeavyUnitUnlocked;
    public bool martianRangedUnitUnlocked;
    public bool martianDodgerUnitUnlocked;
    public bool martianSupportUnitUnlocked;

    // Upgrade Levels (START AT 0)
    public int robotPlayerLevel;
    public int robotBasicUnitLevel;
    public int robotSwarmUnitLevel;
    public int robotRangedUnitLevel;
    public int robotKnockbackUnitLevel;
    public int robotSupportUnitLevel;

    public int martianPlayerLevel;
    public int martianBasicUnitLevel;
    public int martianHeavyUnitLevel;
    public int martianRangedUnitLevel;
    public int martianDodgerUnitLevel;
    public int martianSupportUnitLevel;

    public int wallLevel;
    public int baseLevel;
}
