using UnityEngine;

namespace DBGA.AI.Common
{
    public interface IStorm
    {
        public Vector3 GetCenter();
        public float GetRadius();
        public void StartStorm();
        public void StopStorm();
        public int GetDamageAmout();
    }
}
