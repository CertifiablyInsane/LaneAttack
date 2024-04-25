using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : LaneEntity
{

    protected override void Kill()
    {
        Destroy(gameObject);
    }
    public override void Stop() { }
}
