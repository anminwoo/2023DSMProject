namespace Item.Core
{
    public abstract class Passive : Item
    {
        public virtual void OnGet()
        {
            GameManager.Singleton.player.onMove.AddListener(OnMove);
            GameManager.Singleton.player.onFire.AddListener(OnFire);
            GameManager.Singleton.player.onDamage.AddListener(OnDamage);
            GameManager.Singleton.player.onParry.AddListener(OnParry);
        }

        public abstract void OnMove();

        public abstract void OnFire();

        public abstract void OnDamage();

        public abstract void OnParry();
    }
}