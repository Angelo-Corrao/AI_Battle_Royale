using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Pickable
{
    public class AmmoPicker : MonoBehaviour, IInteractable
    {
		private IAmmo ammo;

		void Awake()
		{
			ammo = GetComponent<IAmmo>();
		}

        public void Interact(GameObject obj)
        {            
            if (obj.TryGetComponent<IInventory>(out var inventory))
            {
                inventory?.PickAmmo(ammo);
                Destroy(gameObject);
            }
        }
    }
}
