using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class GetTarget : Node
    {
		private EyesSensor eyeSensor;

		public GetTarget(EyesSensor eyeSensor, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null) 
			: base(ref blackboard, breakConditions)
		{
			this.eyeSensor = eyeSensor;
		}

		public override NodeState Evaluate() 
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
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
			
				nodeState = NodeState.SUCCESS;
				return nodeState;
			}
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
