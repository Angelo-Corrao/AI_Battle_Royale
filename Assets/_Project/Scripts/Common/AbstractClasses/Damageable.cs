using UnityEngine;

namespace DBGA.AI.Common
{
    public abstract class Damageable : MonoBehaviour, IDamageable
    {
        public abstract int GetPriority();
        public abstract int TakeDamage(int damage);

    }
}
