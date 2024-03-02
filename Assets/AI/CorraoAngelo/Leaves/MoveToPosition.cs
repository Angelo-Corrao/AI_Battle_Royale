using DBGA.AI.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class MoveToPosition : Node
    {
		private PlayerMovement playerMovement;
		private Vector3 positionToMove;
		private Vector2 directionToMove = Vector2.zero;

		public MoveToPosition(PlayerMovement playerMovement, ref BlackBoard blackboard) {
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

			if (blackboard.TryGetValueFromDictionary("hittedObstacle", out bool hittedObstacle)) {
				if (hittedObstacle) {
					blackboard.SetValueToDictionary("hittedObstacle", false);
					blackboard.SetValueToDictionary("isAnyNodeRunning", false);
					directionToMove = Vector2.zero;
					nodeState = NodeState.FAILURE;
					return nodeState;
				}
			}

			BehaviorTree agent;
			blackboard.TryGetValueFromDictionary("agent", out agent);
			blackboard.TryGetValueFromDictionary("positionToMove", out positionToMove);
			// Doesn't count the y of the agent
			float actualDistance = (positionToMove - agent.transform.position).sqrMagnitude - agent.transform.position.y;

			Vector3 direction = (positionToMove - agent.transform.position).normalized;
			directionToMove = new Vector2(direction.x, direction.z);

			if (actualDistance > 1) {
				playerMovement.MoveToward(directionToMove);
				playerMovement.SetDirection(directionToMove);

				blackboard.SetValueToDictionary("isAnyNodeRunning", true);
				nodeState = NodeState.RUNNING;
				return nodeState;
			}
			else {
				blackboard.SetValueToDictionary("isAnyNodeRunning", false);
				directionToMove = Vector2.zero;
				nodeState = NodeState.SUCCESS;
				return nodeState;
			}
		}
	}
}
