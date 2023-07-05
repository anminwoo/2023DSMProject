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
                other.transform.GetComponent<Player>().currentActive = this;
            }
        }

        public abstract void OnUse();
    }
}