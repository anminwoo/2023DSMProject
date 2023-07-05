using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ChestData", fileName = "new Chest")]
public class ChestData : ScriptableObject
{
    public WeightedRandom<Scripts_Baek.Item.Core.Item> spawnableItems;
    public AnimationClip animationClip;
    public Sprite chestSprite;
}