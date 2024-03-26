using DBGA.AI.Common;
using UnityEngine;
using UnityEngine.AI;

namespace DBGA.AI.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 10;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void MoveToward(Vector3 position)
        {
            agent.isStopped = false;
            agent.SetDestination(position);
        }

        public void StopAgent()
        {
            agent.isStopped = true;
        }

        public void SetDirection(Vector2 direction)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        }

        public void Rotate()
        {
			transform.rotation = Quaternion.LookRotation(agent.transform.forward);
		}

        public bool IsPointReachable(Vector3 position)
        {
            NavMeshHit hit;
            if(NavMesh.SamplePosition(position, out hit, 0.1f, NavMesh.AllAreas))
                return true;
            else
                return false;
        }
    }
}
