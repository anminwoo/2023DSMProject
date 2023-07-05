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
        NotChange,
        Sword
    }
    public abstract class Passive : Item
    {
        public PassiveType Type;

        public ChangeStatus Change;

        public AttackType attackType;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                GameManager.Singleton.AddItem(this);
                OnGet();
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
        

        public virtual void OnGet()
        {
            GameManager.Singleton.player.onMove.AddListener(OnMove);
            GameManager.Singleton.player.onFire.AddListener(OnFire);
            GameManager.Singleton.player.onDamage.AddListener(OnDamage);
            GameManager.Singleton.player.onParry.AddListener(OnParry);
            AudioManager.instance.playSfx(AudioManager.Sfx.ItemGet);
        }

        public abstract void OnMove();

        public abstract void OnFire();

        public abstract void OnDamage(Enemy e);

        public abstract void OnParry(Enemy e);
    }
}