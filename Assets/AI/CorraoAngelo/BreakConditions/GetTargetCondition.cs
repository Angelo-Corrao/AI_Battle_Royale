using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class GetTargetCondition : BreakConditions
    {
		private EyesSensor eyeSensor;

		public GetTargetCondition(EyesSensor eyeSensor, ref BlackBoard blackboard)
			: base(ref blackboard)
		{
			this.eyeSensor = eyeSensor;
		}

		public override NodeState Evaluate()
		{
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
			blackboard.SetValueToDictionary("isAnyNodeRunning", false);
			
			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
