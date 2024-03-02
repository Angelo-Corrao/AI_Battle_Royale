using UnityEngine;

namespace DBGA.AI.Common
{
    public interface IMover
    {
        public void MoveToward(Vector2 direction);
        public void SetDirection(Vector2 direction);
    }
}
