namespace Prefabs.Items.Passive.Debug
{
    public class DebugItem : Scripts_Baek.Item.Core.Passive
    {
        public override void OnMove()
        {
            UnityEngine.Debug.Log("디버그 : 플레이어 이동");
        }

        public override void OnFire()
        {
            UnityEngine.Debug.Log("디버그 : 플레이어 공격");
        }

        public override void OnDamage(Enemy e)
        {
            UnityEngine.Debug.Log($"디버그 : 플레이어 피해 입음 by {e}");
        }

        public override void OnParry(Enemy e)
        {
            UnityEngine.Debug.Log($"디버그 : 플레이어 공격 막음 from {e}");
        }
    }
}