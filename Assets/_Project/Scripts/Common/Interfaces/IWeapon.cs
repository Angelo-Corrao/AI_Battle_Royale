namespace DBGA.AI.Common
{
    public interface IWeapon
    {
        public void Shoot();
        public void Reload(int ammoCount);
        public void Enable();
        public void Disable();
        public bool IsOutOfAmmo();
        public int GetMaxAmmo();
        public int GetCurrentAmmo();
		public float GetFireRate();
        public int GetDamage();
        public void Drop();
    }
}
