namespace DBGA.AI.Common
{
    public interface IArmor : IDamageable
    {
        public int GetCurrentDurability();
        public void Drop();
    }
}
