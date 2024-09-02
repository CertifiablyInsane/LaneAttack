using UnityEngine;
public enum Lane
{
    BOT,
    MID,
    TOP,
}
public enum EnemySpawnType
{
    ALWAYS,
    ONLY_DURING_INTRO,
    ONLY_AFTER_INTRO,
    ONLY_AFTER_FIRST_LOOP,
}
public enum Scene
{
    MAIN_MENU,
    LEVEL_SELECT,
    UPGRADES,
    LEVEL
}
public enum UIPositionerType
{
    RELATIVE_TO_SCREEN,
    RELATIVE_TO_PARENT,
    RELATIVE_TO_SELF,
}
public enum SquareResizeType
{
    UNCONSTRAINED,
    HEIGHT_MATCHES_WIDTH,
    WIDTH_MATCHES_HEIGHT
}
public enum AnchorPresets
{
    TOP_LEFT,
    TOP_CENTER,
    TOP_RIGHT,
    BOTTOM_LEFT,
    BOTTOM_CENTER,
    BOTTOM_RIGHT,
    CENTER_LEFT,
    CENTER,
    CENTER_RIGHT
}
public enum Stat
{
    HEALTH,
    DAMAGE,
    SPEED,
    HEAL_DELAY,
    HEAL_INTERVAL
}

public static class Enum
{
    public static string LaneToString(Lane lane)
    {
        return lane switch
        {
            Lane.BOT => "LaneBot",
            Lane.MID => "LaneMid",
            Lane.TOP => "LaneTop",
            _ => null,
        };
    }
    public static string SceneToString(Scene scene)
    {
        return scene switch
        {
            Scene.MAIN_MENU => "Main Menu",
            Scene.LEVEL_SELECT => "Level Select",
            Scene.UPGRADES => "Upgrades",
            Scene.LEVEL => "Level",
            _ => throw new System.Exception("Error: Scene Enum does not have an equivalent string!"),
        };
    }
    public static string StatToString(Stat stat)
    {
        return stat switch
        {
            Stat.HEALTH => "HEALTH",
            Stat.DAMAGE => "DAMAGE",
            Stat.SPEED => "SPEED",
            Stat.HEAL_DELAY => "AUTO-HEAL DELAY",
            Stat.HEAL_INTERVAL => "AUTO-HEAL RATE",
            _ => throw new System.Exception("Error: Stat Enum does not have an equivalent string!")
        };
    }
}
