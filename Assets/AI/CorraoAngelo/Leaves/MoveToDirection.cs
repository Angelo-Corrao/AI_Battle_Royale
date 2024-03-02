using DBGA.AI.Movement;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class MoveToDirection : Node {
		private PlayerMovement playerMovement;
		private float totMoveTime;
		private float time = 0;
		private Vector2 randomDirection;

		public MoveToDirection(PlayerMovement playerMovement, float totMoveTime, ref BlackBoard blackboard) {
			this.playerMovement = playerMovement;
			this.totMoveTime = totMoveTime;
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

			if (nodeState != NodeState.RUNNING)
				randomDirection = Random.insideUnitCircle.normalized;

			if (blackboard.TryGetValueFromDictionary("hasToStop", out bool result)) {
				if (result) {
					blackboard.SetValueToDictionary("hasToStop", false);
					time = 0;

					blackboard.SetValueToDictionary("isAnyNodeRunning", false);
					nodeState = NodeState.FAILURE;
					return nodeState;
				}
			}

			if (time >= totMoveTime) {
				time = 0;

				blackboard.SetValueToDictionary("isAnyNodeRunning", false);
				nodeState = NodeState.SUCCESS;
				return nodeState;
			}
			else {
				time += Time.deltaTime;
				playerMovement.MoveToward(randomDirection);
				playerMovement.SetDirection(randomDirection);
				
				nodeState = NodeState.RUNNING;
				blackboard.SetValueToDictionary("isAnyNodeRunning", true);
				blackboard.SetValueToDictionary("dirToLook", randomDirection);
				return nodeState;
			}
		}
	}
}
