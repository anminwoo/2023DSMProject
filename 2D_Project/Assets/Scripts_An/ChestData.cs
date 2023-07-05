using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ChestData", fileName = "new Chest")]
public class ChestData : ScriptableObject
{
    public WeightedRandom<Scripts_Baek.Item.Core.Item> spawnableItems;
}