using System;
using UnityEngine;

namespace Scripts_Baek.Item.Core
{
    [Flags]
    public enum PassiveType
    {
        Nothing,
        StatusChange = 1 << 0,
        AttackChange = 1 << 1,
        AddPet = 1 << 2
    }

    public enum AttackType
    {
        Sword
    }
    public abstract class Passive : Item
    {
        [SerializeField] protected PassiveType type;

        [SerializeField] protected ChangeStatus change;
        public PassiveType Type => type;
        public ChangeStatus Change => change;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                GameManager.Singleton.AddItem(this);
            }
        }

        public virtual void OnGet()
        {
            GameManager.Singleton.player.onMove.AddListener(OnMove);
            GameManager.Singleton.player.onFire.AddListener(OnFire);
            GameManager.Singleton.player.onDamage.AddListener(OnDamage);
            GameManager.Singleton.player.onParry.AddListener(OnParry);
        }

        public abstract void OnMove();

        public abstract void OnFire();

        public abstract void OnDamage(Enemy e);

        public abstract void OnParry(Enemy e);
    }
}