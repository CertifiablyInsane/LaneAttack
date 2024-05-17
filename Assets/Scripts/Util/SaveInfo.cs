using System;
using UnityEngine;

[Serializable]
public class SaveInfo
{
    public string username;
    public int level;
    public int points;

    // Upgrade Levels (START AT 0)
    public int playerLevel;
    public int basicUnitLevel;
    public int triangleLevel;
    public int wallLevel;
    public int baseLevel;
}
