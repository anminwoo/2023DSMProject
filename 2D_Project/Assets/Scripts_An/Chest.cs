using Scripts_Baek.Item.Core;
using UnityEngine;

namespace Scripts_An
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private ChestData chestData;
        [SerializeField] private bool isOpen;

        private Item _item;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private AnimationClip _clip;

        private void Start()
        {
            _item = chestData.spawnableItems.GetRandom();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _spriteRenderer.sprite = chestData.chestSprite;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        public void Interact()
        {
            if (isOpen)
            {
                return;
            }
            Debug.Log("open");
            Open();
        }

        public void Open()
        {
            isOpen = true;
            _animator.SetTrigger("open");
            // Item.Core.Item spawnItem = Instantiate(item, transform.position, quaternion.identity);
            // spawnItem.Initialize();
        }
    }
}
