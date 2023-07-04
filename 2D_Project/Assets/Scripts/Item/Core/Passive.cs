namespace Item.Core
{
    public abstract class Passive : Item
    {
        public abstract void IOnGet();
        public abstract void IOnMove();
        public abstract void IOnDamage();
        public abstract void IOnFire();
    }
}