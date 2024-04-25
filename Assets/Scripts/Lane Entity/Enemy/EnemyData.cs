using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject spawnEntity;
    public int cost;
}
