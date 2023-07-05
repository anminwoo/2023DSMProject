using Scripts_Baek.Item.Core;
using UnityEngine;

namespace Scripts_Baek.Item.Passives
{
    public class TestItem : Passive
    {
        public override void OnGet()
        {
            Debug.Log("디버그 아이템 획득");
        }

        public override void OnMove()
        {
            Debug.Log("디버그 : 플레이어 이동");
        }

        public override void OnFire()
        {
            Debug.Log("디버그 : 플레이어 공격");
        }

        public override void OnDamage()
        {
            Debug.Log($"디버그 : 플레이어 피해 입음({GameManager.Singleton.player.finalDamage})");
        }

        public override void OnParry()
        {
            Debug.Log("디버그 : 플레이어 공격 쳐냄");
        }
    }
}