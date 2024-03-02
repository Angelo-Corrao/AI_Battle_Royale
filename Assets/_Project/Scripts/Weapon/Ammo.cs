using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Weapon
{
    [DisallowMultipleComponent]
    public class Ammo : MonoBehaviour, IAmmo
    {
        [SerializeField]
        private int ammoCount;

        public int GetAmmoCount()
        {
            return ammoCount;
        }
    }
}
