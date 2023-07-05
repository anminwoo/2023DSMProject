using UnityEngine;

namespace Scripts_Baek.Item.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemInfo itemInfo;
        public ItemInfo ItemInfo => itemInfo;

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize()
        {
            _spriteRenderer.sprite = itemInfo.ItemIcon;
        }
    }
}