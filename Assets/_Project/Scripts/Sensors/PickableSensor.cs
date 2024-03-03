using DBGA.AI.Pickable;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.Sensors
{
    public class PickableSensor : MonoBehaviour
    {
        [SerializeField]
        private float range = 5;
        [SerializeField]
        private LayerMask pickableLayer;

        public List<GameObject> GetNearWeapons()
        {
            return GetNearPickables<WeaponPicker>();
        }

        public List<GameObject> GetNearArmors()
        {
            return GetNearPickables<ArmorPicker>();
        }

        public List<GameObject> GetNearAmmos()
        {
            return GetNearPickables<AmmoPicker>();
        }

        private List<GameObject> GetNearPickables<T>()
        {
            var nearPickables = new List<GameObject>();
            var hitColliders = Physics.OverlapSphere(transform.position, range, pickableLayer);

            foreach (var collider in hitColliders)
            {
                if (!collider.TryGetComponent<T>(out _))
                    continue;
                
				Vector3 directionTarget = (collider.transform.position - transform.position).normalized;

				RaycastHit hit;
                if (Physics.Raycast(transform.position, directionTarget, out hit, range))
                {
                    if (hit.collider.TryGetComponent<T>(out _))
                        nearPickables.Add(collider.gameObject);
                }
			} 

            return nearPickables;
        }
    }
}
