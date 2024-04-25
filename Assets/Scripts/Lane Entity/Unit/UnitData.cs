using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UnitData")]
public class UnitData : ScriptableObject
{
    public GameObject spawnEntity;
    public Sprite icon;
    public int cost;
    public int cooldownTime;
}
