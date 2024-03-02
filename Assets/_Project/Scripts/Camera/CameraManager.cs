using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DBGA.AI.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Transform followAgent;

        [SerializeField]
        private Vector3 camerOffset;
        private float timer = 0;
        private List<GameObject> players;
        private void LateUpdate()
        {
            if (timer <= 0)
            {
                players = GameObject.FindGameObjectsWithTag("Player").ToList();
                followAgent = players[Random.Range(0,players.Count)].transform;
                timer = 10;
            }
            if (followAgent)
                transform.position = followAgent.position + camerOffset;
            else
                timer = 0;
            timer -= Time.deltaTime;
        }

        public void SetTransform(Transform transform)
        {
            followAgent = transform;
        }
    }
}
