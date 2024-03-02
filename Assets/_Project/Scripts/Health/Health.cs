using DBGA.AI.Common;
using UnityEngine;
using UnityEngine.Events;

namespace DBGA.AI.Health
{
    public class Health : Damageable
    {
        [SerializeField]
        private int maxHealth;

        [SerializeField]
        private IInventory inventory;

        [SerializeField]
        private int damagePriority;

        public UnityEvent<int> onHealthUpdated;
        public UnityEvent<Health> onPlayerDies;

        private int currentHealth;

        void Awake()
        {
            currentHealth = maxHealth;
            onHealthUpdated = new UnityEvent<int>();
            onPlayerDies = new UnityEvent<Health>();
        }

        public override int TakeDamage(int damage)
        {
            if (currentHealth >= damage)
            {
                DecreaseHealth(damage);
                return 0;
            }
            else
            {
                int toReturn = damage - currentHealth;
                DecreaseHealth(damage);
                return toReturn;
            }

        }
        public override int GetPriority()
        {
            return damagePriority;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        private void DecreaseHealth(int damage)
        {
            currentHealth -= (damage);
            currentHealth = Mathf.Max(currentHealth, 0);
            onHealthUpdated?.Invoke(currentHealth);
            if (currentHealth == 0)
                onPlayerDies?.Invoke(this);
        }

    }
}
