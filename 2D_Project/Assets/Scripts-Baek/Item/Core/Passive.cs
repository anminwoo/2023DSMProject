namespace Item.Core
{
    public abstract class Passive : Item
    {
        public abstract void OnGet();

        public abstract void OnMove();

        public abstract void OnFire();

        public abstract void OnDamage();

        public abstract void OnParry();
    }
}