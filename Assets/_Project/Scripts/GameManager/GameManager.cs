using DBGA.AI.Common;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.GameManager
{
    using DBGA.AI.DamageWrapper;
    using DBGA.AI.Health;
    public class GameManager : MonoBehaviour
    {
		[SerializeField]
		private GameObject stormObject;
		[SerializeField]
		private List<GameObject> aiAgentsPrefabs;

		private IStorm storm;

        private int currentAgentCount;
        private bool isGameEnded = false;
        private List<Transform> spawnLocation;

        void Awake()
        {
            storm = stormObject.GetComponent<IStorm>();
            spawnLocation = new List<Transform>();

            foreach (Transform child in transform)
                spawnLocation.Add(child);
        }

        void Start()
        {
            storm.StartStorm();

            for (int i = 0; i < aiAgentsPrefabs.Count; i++)
            {
                GameObject player = Instantiate(aiAgentsPrefabs[i], spawnLocation[i].position, Quaternion.identity);
                Health playerHealth = player.GetComponent<Health>();
                playerHealth.onPlayerDies.AddListener(CheckGameEnded);
                DamageWrapper damageWrapper = player.GetComponent<DamageWrapper>();
            }
        }

        public void CheckGameEnded(Health player)
        {
            currentAgentCount--;
            if (currentAgentCount <= 1)
                isGameEnded = true;
            player.onPlayerDies.RemoveAllListeners();
            Destroy(player.gameObject);
        }

        public bool IsGameEnded()
        {
            return isGameEnded;
        }

        public int GetCurrentAgentCount()
        {
            return currentAgentCount;
        }
    }
}
