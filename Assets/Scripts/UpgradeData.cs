using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeData
{
    public static int UpgradeVarsToIgnoreForStatDisplay = 2;
    /*
     *  ROBOT PLAYER SECTION
     */

    // Robot Player

    private static Upgrade[] _robotPlayerUpgrades;
    public static Upgrade[] RobotPlayerUpgrades
    {
        get
        {
            _robotPlayerUpgrades ??= CreateRobotPlayerUpgrades();
            return _robotPlayerUpgrades;
        }
    }
    private static Upgrade[] CreateRobotPlayerUpgrades()
    {
        return new Upgrade[]
        {
            // LEVEL 01
            new() {
                level = 1,  pointsForNextUpgrade = 300, health = 6, damage = 1,
                healDelay = 8,  healInterval = 1.25f,   speed = 3,
            },
            // LEVEL 02
            new() {
                level = 2,  pointsForNextUpgrade = 900, health = 7, damage = 1,
                healDelay = 7,  healInterval = 1.25f,   speed = 3.5f,
            },
            // LEVEL 03
            new() {
                level = 3,  pointsForNextUpgrade = 2700, health = 8, damage = 1,
                healDelay = 8,  healInterval = 1,   speed = 3.75f,
            },
            // LEVEL 04
            new() {
                level = 4,  pointsForNextUpgrade = 6500, health = 9, damage = 2,
                healDelay = 6,  healInterval = 1,   speed = 4,
            },
            // LEVEL 05
            new() {
                level = 5,  pointsForNextUpgrade = 14000, health = 10, damage = 2,
                healDelay = 6,  healInterval = .75f,   speed = 4,
            },
            // LEVEL 06
            new() {
                level = 6,  pointsForNextUpgrade = 23000, health = 12, damage = 2,
                healDelay = 5,  healInterval = .65f,   speed = 4,
            },
            // LEVEL 07
            new() {
                level = 7,  pointsForNextUpgrade = 34000, health = 14, damage = 2,
                healDelay = 5,  healInterval = .60f,   speed = 4.25f,
            },
            // LEVEL 08
            new() {
                level = 8,  pointsForNextUpgrade = 48000, health = 17, damage = 3,
                healDelay = 4,  healInterval = .60f,   speed = 4.25f,
            },
            // LEVEL 09
            new() {
                level = 9,  pointsForNextUpgrade = 64000, health = 20, damage = 3,
                healDelay = 4,  healInterval = .50f,   speed = 4.25f,
            },
            // LEVEL 10
            new() {
                level = 10,  pointsForNextUpgrade = 85000, health = 23, damage = 3,
                healDelay = 3,  healInterval = .45f,   speed = 4.25f,
            },
            // LEVEL 11
            new() {
                level = 11,  pointsForNextUpgrade = 100000, health = 25, damage = 3,
                healDelay = 3,  healInterval = .35f,   speed = 4.75f,
            },
            // LEVEL 12
            new() {
                level = 12,  pointsForNextUpgrade = 140000, health = 26, damage = 4,
                healDelay = 2,  healInterval = .35f,   speed = 5,
            },
            // LEVEL 13
            new() {
                level = 13,  pointsForNextUpgrade = 165000, health = 27, damage = 4,
                healDelay = 2,  healInterval = .25f,   speed = 5,
            },
            // LEVEL 14
            new() {
                level = 14,  pointsForNextUpgrade = 200000, health = 28, damage = 4,
                healDelay = 1,  healInterval = .25f,   speed = 5.5f,
            },
            // LEVEL 15
            new() {
                level = 15,  pointsForNextUpgrade = -1, health = 30, damage = 5,
                healDelay = 1,  healInterval = .25f,   speed = 6,
            },
        };
    }

    // Robot Basic Unit

    private static Upgrade[] _robotBasicUnitUpgrades;
    public static Upgrade[] RobotBasicUnitUpgrades
    {
        get
        {
            _robotBasicUnitUpgrades ??= CreateRobotBasicUnitUpgrades();
            return _robotBasicUnitUpgrades;
        }
    }
    private static Upgrade[] CreateRobotBasicUnitUpgrades()
    {
        return new Upgrade[]
        {
            // LEVEL 01
            new() {
                level = 1,  pointsForNextUpgrade = 100,
                health = 3, damage = 1, speed = 2,
            },
            // LEVEL 02
            new() {
                level = 2,  pointsForNextUpgrade = 500,
                health = 4, damage = 1, speed = 2,
            },
            // LEVEL 03
            new() {
                level = 3,  pointsForNextUpgrade = 2700,
                health = 5, damage = 1, speed = 2.25f,
            },
            // LEVEL 04
            new() {
                level = 4,  pointsForNextUpgrade = 6500,
                health = 6, damage = 2, speed = 2.25f,
            },
            // LEVEL 05
            new() {
                level = 5,  pointsForNextUpgrade = 10000,
                health = 8, damage = 2, speed = 2.50f,
            },
            // LEVEL 06
            new() {
                level = 6,  pointsForNextUpgrade = 25000,
                health = 9, damage = 2, speed = 3,
            },
            // LEVEL 07
            new() {
                level = 7,  pointsForNextUpgrade = 40000,
                health = 11, damage = 2, speed = 3,
            },
            // LEVEL 08
            new() {
                level = 8,  pointsForNextUpgrade = 60000,
                health = 13, damage = 3, speed = 3,
            },
            // LEVEL 09
            new() {
                level = 8,  pointsForNextUpgrade = 75000,
                health = 14, damage = 3, speed = 3.5f,
            },
            // LEVEL 10
            new() {
                level = 8,  pointsForNextUpgrade = -1,
                health = 15, damage = 4, speed = 4,
            },
        };
    }

    // Robot Swarm Unit

    // Robot Ranged Unit

    // Robot Knockback Unit

    // Robot Ranged Unit


    /*
     *  MARTIAN PLAYER SECTION
     */

    public static Upgrade[] GetUpgradeArrayFromUnitData(UnitData data)
    {
        UnitDataHelper udh = GameManager.UnitData;

        return data switch
        {
            var _ when data.Equals(udh.robotBasicUnitData) => RobotBasicUnitUpgrades,
            var _ when data.Equals(udh.robotSwarmUnitData) => RobotBasicUnitUpgrades,
            var _ when data.Equals(udh.robotRangedUnitData) => null,
            var _ when data.Equals(udh.robotKnockbackUnitData) => null,
            var _ when data.Equals(udh.robotSupportUnitData) => null,
            var _ when data.Equals(udh.martianBasicUnitData) => null,
            var _ when data.Equals(udh.martianHeavyUnitData) => null,
            var _ when data.Equals(udh.martianRangedUnitData) => null,
            var _ when data.Equals(udh.martianDodgerUnitData) => null,
            var _ when data.Equals(udh.martianSupportUnitData) => null,
            _ => throw new System.Exception("Could Not Get Upgrade Array from Unit Data!"),
        };
    }
    public static Upgrade[] GetUpgradeArrayFromProtagonistData(ProtagonistData data)
    {
        ProtagonistDataHelper pdh = GameManager.ProtagonistData;
        return data switch
        {
            var _ when data.Equals(pdh.robotProtagonistData) => RobotPlayerUpgrades,
            var _ when data.Equals(pdh.martianProtagonistData) => null,
            _ => throw new System.Exception("Could Not Get Upgrade Array from Protagonist Data!"),
        };
    }
}
public struct Upgrade
{
    // IGNORE IN UNIT STAT DISPLAY
    public int level;               
    public int pointsForNextUpgrade;
    // DISPLAY IN UNIT STAT DISPLAY
    public int health;
    public int damage;
    public float speed;
    public float healDelay;
    public float healInterval;
}