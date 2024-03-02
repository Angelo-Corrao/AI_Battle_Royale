using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class IsInTheStorm : Node
    {
		protected Node childNode;
		private Storm.Storm storm;

		public IsInTheStorm(Node childNode, Storm.Storm storm, ref BlackBoard blackboard) {
			this.childNode = childNode;
			this.storm = storm;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate() {
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool result)) {
				if (result) {
					if (nodeState == NodeState.RUNNING) {
						nodeState = childNode.Evaluate();
						return nodeState;
					}
					else {
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
				}
			}

			BehaviorTree agent;
			blackboard.TryGetValueFromDictionary("agent", out agent);

			Vector3 center = storm.GetCenter();
			float radius = storm.GetRadius();

			float distance = (center - agent.transform.position).magnitude;

			if (distance <= radius)
				nodeState = NodeState.FAILURE;
			else {
				// Generate a random point inside the safe area
				Vector2 randomDirection = Random.insideUnitCircle.normalized;
				float randomLenght = Random.Range(1, radius);
				Vector3 randomPointInsideSafe = center + new Vector3(randomDirection.x, 0, randomDirection.y) * randomLenght;
				Vector3 dirToMove = (randomPointInsideSafe - agent.transform.position).normalized;
				Vector2 dirToLook = new Vector2(dirToMove.x, dirToMove.z);

				blackboard.SetValueToDictionary("dirToLook", dirToLook);
				blackboard.SetValueToDictionary("positionToMove", randomPointInsideSafe);
				nodeState = childNode.Evaluate();
			}

			return nodeState;
		}
	}
}
