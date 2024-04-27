using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : LaneEntity
{
    private enum Role
    {
        NONE,
        ENEMY_BASE,
        PLAYER_BASE
    }
    [SerializeField] private Role role; 
    // Events
    public delegate void ObstacleEvent(Obstacle obstacle);
    public static event ObstacleEvent OnObstacleKilled;
    public static event ObstacleEvent OnEnemyBaseKilled;
    public static event ObstacleEvent OnPlayerBaseKilled;
    protected override void Kill()
    {
        switch (role)
        {
            case Role.NONE:
                OnObstacleKilled?.Invoke(this);
                break;
            case Role.ENEMY_BASE:
                OnEnemyBaseKilled?.Invoke(this);
                break;
            case Role.PLAYER_BASE:
                OnPlayerBaseKilled?.Invoke(this);
                break;
        }
        Destroy(gameObject);
    }
    public override void Stop() { }
}
