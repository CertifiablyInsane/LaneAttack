using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UnitData")]
public class UnitData : ScriptableObject
{
    public GameObject spawnEntity;
    public string displayName;
    public Sprite icon;
    public int cost;
    public int cooldownTime;
}
