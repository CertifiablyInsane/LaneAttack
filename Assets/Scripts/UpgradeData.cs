using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : Singleton<UpgradeData>
{
    public static PlayerUpgrade[] playerUpgrades;
    private static bool staticInitialized = false;

    private void Start()
    {
        
    }
    private static void StaticInitialize()
    {
        playerUpgrades = new PlayerUpgrade[]
        {
            // LEVEL 01
            new PlayerUpgrade {
                level = 1,  pointsRequired = 0, health = 6, damage = 1,
                healDelay = 8,  healInterval = 1.25f,   speed = 3,
            },
            // LEVEL 02
            new PlayerUpgrade {
                level = 2,  pointsRequired = 300, health = 7, damage = 1,
                healDelay = 7,  healInterval = 1.25f,   speed = 3.5f,
            },
            // LEVEL 03
            new PlayerUpgrade {
                level = 3,  pointsRequired = 900, health = 8, damage = 1,
                healDelay = 8,  healInterval = 1,   speed = 3.75f,
            },
            // LEVEL 04
            new PlayerUpgrade {
                level = 4,  pointsRequired = 2700, health = 9, damage = 2,
                healDelay = 6,  healInterval = 1,   speed = 4,
            },
            // LEVEL 05
            new PlayerUpgrade {
                level = 5,  pointsRequired = 6500, health = 10, damage = 2,
                healDelay = 6,  healInterval = .75f,   speed = 4,
            },
            // LEVEL 06
            new PlayerUpgrade {
                level = 6,  pointsRequired = 14000, health = 12, damage = 2,
                healDelay = 5,  healInterval = .65f,   speed = 4,
            },
            // LEVEL 07
            new PlayerUpgrade {
                level = 7,  pointsRequired = 23000, health = 14, damage = 2,
                healDelay = 5,  healInterval = .60f,   speed = 4.25f,
            },
            // LEVEL 08
            new PlayerUpgrade {
                level = 8,  pointsRequired = 34000, health = 17, damage = 3,
                healDelay = 4,  healInterval = .60f,   speed = 4.25f,
            },
            // LEVEL 09
            new PlayerUpgrade {
                level = 9,  pointsRequired = 48000, health = 20, damage = 3,
                healDelay = 4,  healInterval = .50f,   speed = 4.25f,
            },
            // LEVEL 10
            new PlayerUpgrade {
                level = 10,  pointsRequired = 64000, health = 23, damage = 3,
                healDelay = 3,  healInterval = .45f,   speed = 4.25f,
            },
            // LEVEL 11
            new PlayerUpgrade {
                level = 11,  pointsRequired = 85000, health = 25, damage = 3,
                healDelay = 3,  healInterval = .35f,   speed = 4.75f,
            },
            // LEVEL 12
            new PlayerUpgrade {
                level = 12,  pointsRequired = 100000, health = 26, damage = 4,
                healDelay = 2,  healInterval = .35f,   speed = 5,
            },
            // LEVEL 13
            new PlayerUpgrade {
                level = 13,  pointsRequired = 140000, health = 27, damage = 4,
                healDelay = 2,  healInterval = .25f,   speed = 5,
            },
            // LEVEL 14
            new PlayerUpgrade {
                level = 14,  pointsRequired = 165000, health = 28, damage = 4,
                healDelay = 1,  healInterval = .25f,   speed = 5.5f,
            },
            // LEVEL 15
            new PlayerUpgrade {
                level = 15,  pointsRequired = 200000, health = 30, damage = 5,
                healDelay = 1,  healInterval = .25f,   speed = 6,
            },
        };
        staticInitialized = true;
    }
}

public struct PlayerUpgrade
{
    public int level;
    public int pointsRequired;
    public int health;
    public int damage;
    public float healDelay;
    public float healInterval;
    public float speed;
}