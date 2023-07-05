using System;
using Scripts_ojy.Player;
using UnityEngine;

namespace Scripts_Baek.Item.Core
{
    public abstract class Active : Item
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                if (GameManager.Singleton.player.currentActive != null) ItemSpawnSystem.Singleton.SpawnItem(GameManager.Singleton.player.currentActive.gameObject, transform);
                GameManager.Singleton.player.currentActive = this;
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        public abstract void OnUse();
    }
}