    namespace DBGA.AI.Common
{
    public interface IDamageable
    {
        public int GetPriority();
        public int TakeDamage(int damage);
    }
}
