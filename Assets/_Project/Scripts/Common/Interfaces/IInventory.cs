namespace DBGA.AI.Common
{
    public interface IInventory
    {
        public void PickWeapon(IWeapon weapon);
        public void PickArmor(IArmor armor);
        public void PickAmmo(IAmmo ammoCount);
        public IWeapon GetActiveWeapon();
        public IWeapon GetBackupWeapon();
		public IArmor GetCurrentArmor();
        public void SwapWeapons();
    }
}
