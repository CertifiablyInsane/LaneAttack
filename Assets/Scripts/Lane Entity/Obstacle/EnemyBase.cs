using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : LaneEntity
{
    protected override void Kill()
    {
        LevelManager.Instance.CompleteLevel();
    }

    public override void Stop() { }
}
