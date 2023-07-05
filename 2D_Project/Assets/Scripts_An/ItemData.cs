using UnityEngine;

namespace Scripts_An
{
    [CreateAssetMenu(menuName = "ItemData", fileName = "ItemData")]
    public class ItemData : ScriptableObject
    {
        public int damage;
        public int hp;
        public float speed;
    }
}
