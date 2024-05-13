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
    public static AnchorPresets GetAnchorPresetFromVector(Vector2 pos)
    {
        if (pos == Vector2.zero) return AnchorPresets.BOTTOM_LEFT;
        if (pos == Vector2.right * .5f) return AnchorPresets.BOTTOM_CENTER;
        if (pos == Vector2.right) return AnchorPresets.BOTTOM_RIGHT;
        if (pos == Vector2.up) return AnchorPresets.TOP_LEFT;
        if (pos == new Vector2(.5f, 1f)) return AnchorPresets.TOP_CENTER;
        if (pos == Vector2.one) return AnchorPresets.TOP_RIGHT;
        if (pos == Vector2.up * .5f) return AnchorPresets.CENTER_LEFT;
        if (pos == Vector2.one * .5f) return AnchorPresets.CENTER;
        if (pos == new Vector2(1f, .5f)) return AnchorPresets.CENTER_RIGHT;

        // If no match
        Debug.LogWarning("No Anchor Preset match could be found");
        return AnchorPresets.CENTER;
    }
}
