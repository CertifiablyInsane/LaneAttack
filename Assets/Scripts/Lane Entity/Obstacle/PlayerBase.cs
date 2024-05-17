using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : LaneEntity
{
    protected override void Kill()
    {
        LevelManager.Instance.FailLevel();
    }
    public override void Stop() { }
}
