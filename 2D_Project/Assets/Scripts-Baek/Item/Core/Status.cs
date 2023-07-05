using System;

namespace Scripts_Baek.Item.Core
{
    [Serializable]
    public struct Status
    {
        public int maxHp;
        public int shield;
        public int damage;
        public float speed;
        public int defensive;
    }

    [Serializable]
    public struct ChangeStatus
    {
        public int maxHp;
        public int shield;
        public int damage;
        public float speed;
        public int defensive;
    }
}