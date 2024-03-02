using DBGA.AI.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class AimToDirection : Node
    {
		private PlayerMovement playerMovement;

		public AimToDirection(PlayerMovement playerMovement, ref BlackBoard blackboard) {
			this.playerMovement = playerMovement;
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

			BehaviorTree agent;
			blackboard.TryGetValueFromDictionary("agent", out agent);

			if (blackboard.TryGetValueFromDictionary("targetEnemy", out GameObject enemy)) {
				Vector3 direction = (enemy.transform.position - agent.transform.position).normalized;
				playerMovement.SetDirection(new Vector2(direction.x, direction.z));

				nodeState = NodeState.SUCCESS;
			}
			else
				nodeState = NodeState.FAILURE;

			return nodeState;
		}
	}
}
