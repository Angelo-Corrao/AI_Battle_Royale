using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Inventory
{
    public class Inventory : MonoBehaviour, IInventory
    {
        public IArmor armor;
        public IWeapon activeWeapon;
        public IWeapon backupWeapon;

		public IWeapon GetBackupWeapon() 
        {
            return backupWeapon;
        }

        public IWeapon GetActiveWeapon()
        {
            return activeWeapon;
        }

        public IArmor GetCurrentArmor()
        {
            return armor;
        }

        public void PickAmmo(IAmmo ammoCount)
        {
            if (ammoCount != null)
                activeWeapon?.Reload(ammoCount.GetAmmoCount());
        }

        public void PickArmor(IArmor armor)
        {
            armor?.Drop();
            this.armor = armor;
        }

        public void PickWeapon(IWeapon weapon)
        {
            if (activeWeapon != null && backupWeapon == null)
                backupWeapon = weapon;
            else
            {
                activeWeapon?.Drop();
                activeWeapon = weapon;
            }
        }

        public void SwapWeapons()
        {
            IWeapon tempActiveWeapon = activeWeapon;
            activeWeapon = backupWeapon;
            backupWeapon = tempActiveWeapon;
        }
    }
}
