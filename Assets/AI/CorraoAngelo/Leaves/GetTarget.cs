using DBGA.AI.Sensors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class GetTarget : Node
    {
		private EyesSensor eyeSensor;

		public GetTarget(EyesSensor eyeSensor, ref BlackBoard blackboard) {
			this.eyeSensor = eyeSensor;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate() {
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool isAnyNodeRunning)) {
				if (isAnyNodeRunning) {
					if (nodeState != NodeState.RUNNING) {
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
				}
			}

			List<GameObject> enemies = new List<GameObject>();
			enemies = eyeSensor.GetEnemiesTargets();

			if (enemies.Count == 0) {
				nodeState = NodeState.FAILURE;
				return nodeState;
			}

			// Calculate nearest enemy
			BehaviorTree agent;
			blackboard.TryGetValueFromDictionary("agent", out agent);
			float nearestDistance = (enemies[0].transform.position - agent.transform.position).sqrMagnitude;
			GameObject nearestEnemy = enemies[0];

			foreach (var enemy in enemies) {
				float distance = (enemy.transform.position - agent.transform.position).sqrMagnitude;
				if (distance < nearestDistance) {
					nearestDistance = distance;
					nearestEnemy = enemy;
				}
			}

			blackboard.SetValueToDictionary("targetEnemy", nearestEnemy);
			
			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
