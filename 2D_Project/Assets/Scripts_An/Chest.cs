using System;
using DG.Tweening;
using Scripts_Baek.Item.Core;
using UnityEngine;

namespace Scripts_An
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private ChestData chestData;
        [SerializeField] private bool isOpen;

        [SerializeField] private float destroyTime;

        [SerializeField] private GameObject item;
        private Animator _animator;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
        //    item = chestData.spawnableItems.GetRandom();
            _animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
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
            _animator.SetTrigger("Open");
            AudioManager.instance.playSfx(AudioManager.Sfx.Chepen);
            ItemSpawnSystem.Singleton.SpawnItem(item, transform);
            spriteRenderer.DOFade(0, destroyTime).OnComplete(() => Destroy(gameObject));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Interact();
            }
        }
    }
}
