using System;
using UnityEngine;

namespace Item.Core
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemInfo itemInfo;
        public ItemInfo ItemInfo => itemInfo;

        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize()
        {
            spriteRenderer.sprite = itemInfo.ItemIcon;
        }
    }
}