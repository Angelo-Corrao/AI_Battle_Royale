using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Pickable
{
    using DBGA.AI.DamageWrapper;
    using DBGA.AI.Inventory;
    using DBGA.AI.Armor;

    public class ArmorPicker : MonoBehaviour, IInteractable
    {
        private const string PLAYERTAG = "Player";

        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Collider armorCollider;
        [SerializeField]
        private Collider armorTrigger;
        [SerializeField]
        private Renderer mesh;

        private IArmor armor;
        private DamageWrapper armorDamageWrapper;
        private Inventory inventory;

        void Awake()
        {
            armor = GetComponent<IArmor>();
        }

        private void Pick(GameObject target)
        {
            armorTrigger.enabled = false;
            transform.parent = target.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
			Armor armorObject = (Armor)armor;
			transform.parent.TryGetComponent<DamageWrapper>(out armorDamageWrapper);
            armorObject.damageWrapper = armorDamageWrapper;
			armorDamageWrapper.armor = armor;
			transform.parent.TryGetComponent<Inventory>(out inventory);
            armorObject.inventory = inventory;
            inventory.armor = armor;
		}

        public void Interact(GameObject obj)
        {
            if (obj.TryGetComponent<IInventory>(out var inventory))
            {
                inventory?.PickArmor(armor);
                Pick(obj);
            }
        }
    }
}
