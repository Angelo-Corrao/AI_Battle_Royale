using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Pickable
{
    public class WeaponPicker : MonoBehaviour, IInteractable
    {
        private const string PLAYERTAG = "Player";

        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Collider weaponCollider;
        [SerializeField]
        private Collider weaponTrigger;

        private IWeapon weapon;

        void Awake()
        {
            weapon = GetComponent<IWeapon>();
        }

        public void Interact(GameObject obj)
        {
            if (obj.TryGetComponent<IInventory>(out var inventory))
            {
                inventory?.PickWeapon(weapon);
                Pick(obj);
            }
        }

        private void Pick(GameObject target)
        {
            transform.parent = target.transform;
            transform.localPosition = Vector3.zero;
            transform.localPosition = new Vector3(transform.localPosition.x + 0.5f, 0.6f, transform.localPosition.z + 0.5f);
            gameObject.layer = 2;
            transform.localRotation = Quaternion.identity;
		}
    }
}
