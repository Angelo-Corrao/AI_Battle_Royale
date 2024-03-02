using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.Sensors
{
    [DisallowMultipleComponent]
    public class CoverSensor : MonoBehaviour
    {
        [SerializeField]
        private float maxCoverDistance = 5;
        [SerializeField]
        private LayerMask coverLayer;

        private List<Vector3> availableCoverPositions;

        void Awake()
        {
            availableCoverPositions = new List<Vector3>();
        }

        void OnDrawGizmos()
        {
            if (availableCoverPositions == null)
                return;

            Gizmos.color = Color.red;
            foreach (var position in availableCoverPositions)
                Gizmos.DrawSphere(position, 0.1f);
        }

        public List<Vector3> FindCoverPositions(Vector3 targetPosition)
        {
            var covers = Physics.OverlapSphere(transform.position, maxCoverDistance, coverLayer);
            availableCoverPositions.Clear();

            foreach (var cover in covers)
            {
                var directionTargetCover = (cover.transform.position - targetPosition).normalized;
                var coverPosition = targetPosition + directionTargetCover *
                    ((cover.transform.position - targetPosition).magnitude +
                    Mathf.Sqrt(
                        cover.bounds.extents.x * cover.bounds.extents.x +
                        cover.bounds.extents.z * cover.bounds.extents.z)
                    + 0.5f);

                availableCoverPositions.Add(coverPosition);
            }

            return availableCoverPositions;
        }
    }
}