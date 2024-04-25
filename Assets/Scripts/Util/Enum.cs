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
}
