using UnityEngine;

namespace Item.Core
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemInfo itemInfo;
        public ItemInfo ItemInfo => itemInfo;
    }
}