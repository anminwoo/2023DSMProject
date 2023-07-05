using System.Collections.Generic;
using Scripts_An;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ChestData", fileName = "new Chest")]
public class ChestData : ScriptableObject
{
    public List<ItemData> itemData;
}