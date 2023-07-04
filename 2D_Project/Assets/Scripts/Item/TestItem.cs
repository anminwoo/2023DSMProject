using Item.Core;
using UnityEngine;

namespace Item
{
    public class TestItem : Passive
    {
        public override void IOnGet()
        {
            
        }

        public override void IOnMove()
        {
            
        }

        public override void IOnDamage()
        {
            
        }

        public override void IOnFire()
        {
            
        }

        [SerializeField] private ChangeStatus changeStatus;
        public ChangeStatus Status
        {
            get => changeStatus;
            set => changeStatus = value;
        }
    }
}