using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ChestData", fileName = "new Chest")]
public class ChestData : ScriptableObject
{
    public WeightedRandom<GameObject> spawnableItems;
}