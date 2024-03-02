using DBGA.AI.Common;
using UnityEngine;
using UnityEngine.Events;

namespace DBGA.AI.Armor
{
    using DBGA.AI.Inventory;
    using DBGA.AI.DamageWrapper;
    public class Armor : MonoBehaviour, IArmor
    {
        [SerializeField]
        private int maxDurability;
        [SerializeField]
        private int priority;
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Collider armorCollider;
        [SerializeField]
        private Collider armorTrigger;
		

        public Inventory inventory;
        public DamageWrapper damageWrapper;
		private int currentDurability;

		void Awake()
        {
            currentDurability = maxDurability;
        }

        public int GetCurrentDurability()
        {
            return currentDurability;
        }

        public int GetPriority()
        {
            return priority;
        }

        public int TakeDamage(int damage)
        {
            int exceedDamage = 0;
            if (currentDurability <= damage)
            {
                exceedDamage = damage - currentDurability;
                currentDurability = 0;
                damageWrapper.armor = null;
                inventory.armor = null;
                Destroy(gameObject);
            }
            else
                currentDurability -= damage;
            return exceedDamage;
        }

        public void Drop()
        {
            transform.position += Vector3.up + Vector3.forward;
            /*rb.useGravity = true;
            armorCollider.enabled = true;*/
            armorTrigger.enabled = true;
            transform.SetParent(null);
        }
    }
}
