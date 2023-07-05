using UnityEngine;

namespace Scripts_Baek.Item.Core
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemInfo itemInfo;
        public ItemInfo ItemInfo => itemInfo;

        private SpriteRenderer _spriteRenderer;
        protected Collider2D collider2D;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            collider2D = GetComponent<Collider2D>();
        }

        public void Initialize()
        {
            _spriteRenderer.sprite = itemInfo.ItemIcon;
        }
    }
}