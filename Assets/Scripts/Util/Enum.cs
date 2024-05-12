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
}
