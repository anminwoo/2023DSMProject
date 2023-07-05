﻿using System.Collections.Generic;
using Scripts_Baek.Item.Core;
using Scripts_ojy;
using Scripts_ojy.Player;
using UnityEngine;

namespace Scripts_Baek
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _singleton;

        public static GameManager Singleton
        {
            get => _singleton;
            private set
            {
                if (_singleton == null)
                {
                    _singleton = value;
                }
                else if (_singleton	!= value)
                {
                    Debug.Log("경고 : 이미 게임메니저가 존재합니다!");
                    Destroy(value.gameObject);
                }
            }
        }

        private void Awake()
        {
            Singleton = this;
        }

        public Player player;
        public GameObject passives;
        public AttackType currentAttackType = AttackType.Sword;
        
        [SerializeField] private List<Passive> _passives;
        
        public List<Passive> GetPassive => _passives;

        public void AddItem(Passive item)
        {
            _passives.Add(item);
            CheckItem();
        }

        public void RemoveItem(int index)
        {
            _passives.RemoveAt(index);
            CheckItem();
        }
        private void CheckItem()
        {
            player.Init(player.statusData.status);
            foreach (Passive p in _passives)
            {
                if ((p.Type & PassiveType.StatusChange) == PassiveType.StatusChange)
                {
                    player.currentStatus.damage += p.Change.damage;
                    player.currentStatus.defensive += p.Change.defensive;
                    player.currentStatus.shield += p.Change.shield;
                    player.currentStatus.speed += p.Change.speed;
                    player.currentStatus.maxHp += p.Change.maxHp;
                    if (player.currentStatus.maxHp <= 0) player.OnDeath();
                    else if (player.currentHp > player.currentStatus.maxHp) player.currentHp = player.currentStatus.maxHp;
                }

                if ((p.Type & PassiveType.AttackChange) == PassiveType.AttackChange)
                {
                    currentAttackType = p.attackType;
                }
            }
        }
    }
}