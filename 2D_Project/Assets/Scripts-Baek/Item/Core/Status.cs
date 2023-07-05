using System;

namespace Item.Core
{
    [Serializable]
    public struct Status
    {
        public uint maxHp;
        public uint shield;
        public uint damage;
        public float speed;
        public uint defensive;
    }

    [Serializable]
    public struct ChangeStatus
    {
        public int maxHp;
        public uint shield;
        public int damage;
        public float speed;
        public int defensive;
    }
}