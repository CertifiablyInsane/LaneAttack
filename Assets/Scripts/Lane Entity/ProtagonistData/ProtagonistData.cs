using UnityEngine;

/**
 *  ScriptableObject to contain data for either protagonist (robot or martian)
 */
[CreateAssetMenu(menuName = "ScriptableObjects/ProtagonistData")]
public class ProtagonistData : ScriptableObject
{
    public GameObject spawnEntity;
    public string displayName;
    public Sprite icon;
}
