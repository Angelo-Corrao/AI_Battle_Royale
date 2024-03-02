using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Sensors
{
    [DisallowMultipleComponent]
    public class StormSensor : MonoBehaviour
    {
        private IStorm storm;

        void Awake()
        {
            storm = GameObject.FindGameObjectWithTag("Storm").GetComponent<IStorm>();
        }

        public IStorm GetStorm()
        {
            return storm;
        }
    }
}
