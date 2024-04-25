using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Public Data")]
    public string levelName;
    public int levelNumber;

    [Header("Wave Series")]
    public int[] wavesIntro;
    public int[] wavesLoop;
    public float timeBetweenWaves;

    [Header("Enemy Data")]
    public EnemyWeight[] enemyWeights;
}

[Serializable]
public struct EnemyWeight
{
    public EnemyData enemy;
    public int weight;
    public EnemySpawnType spawnType;
}
