using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DBGA.AI.Sensors
{
    [DisallowMultipleComponent]
    public class EyesSensor : MonoBehaviour
    {
        [SerializeField, Range(0f, 360f)]
        private float angle = 120f;
        [SerializeField]
        private float range = 5;
        [SerializeField]
        private string enemiesTag;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Handles.color = Color.green;
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
            Handles.DrawWireArc(
                transform.position,
                transform.up,
                Quaternion.Euler(0, -angle / 2, 0) * transform.forward, angle, range);
        }

        public List<GameObject> GetEnemiesTargets()
        {
			List<GameObject> targets = new List<GameObject>();
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

			foreach (var e in hitColliders)
			{
				// Skip if it's ourself or if it's not a Player
				if (e.gameObject == gameObject || !e.gameObject.CompareTag(enemiesTag))
					continue;

				Vector3 directionTarget = (e.transform.position - transform.position).normalized;
				// Skip if it's not in the vision cone
				if (Vector3.Dot(transform.forward, directionTarget) < Mathf.Cos(angle))
					continue;
				
				// Check if there are obstacles between the two players
				RaycastHit hit;
				if (Physics.Raycast(transform.position, directionTarget, out hit, range))
				{
					if (hit.collider.gameObject.CompareTag(enemiesTag))
						targets.Add(e.gameObject);
				}
			}

			return targets;
		}
    }
}

