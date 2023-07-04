using System;
using UnityEngine;

namespace Item.Core
{
    public enum ItemGrade
    {
        Common,
        Rare,
        Epic,
        Legend
    }
    [Flags]
    public enum ItemSpawn
    {
        Nothing,
        Box,
        Shop,
        Boss
    }
    [CreateAssetMenu(menuName = "Item/ItemInfo", fileName = "new ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        [Header("Visible")] 
        
        [SerializeField] private Sprite itemIcon;
        public Sprite ItemIcon => itemIcon;

        [SerializeField] private ItemGrade itemGrade;
        public ItemGrade ItemGrade => itemGrade;
        
        [SerializeField] private string itemName;
        public string ItemName => itemName;

        [SerializeField, TextArea] private string itemDescription;
        public string ItemDescription => itemDescription;

        [Header("Invisible")] 
        
        [SerializeField] private uint itemCost;
        public uint ItemCost => itemCost;

        [SerializeField] private ItemSpawn itemSpawn;
        public ItemSpawn ItemSpawn => itemSpawn;
    }
}