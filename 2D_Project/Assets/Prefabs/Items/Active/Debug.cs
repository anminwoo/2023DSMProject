namespace Prefabs.Items.Active
{
    public class Debug : Scripts_Baek.Item.Core.Active
    {
        public override void OnUse()
        {
            UnityEngine.Debug.Log("디버그 : 아이템 사용");
        }
    }
}