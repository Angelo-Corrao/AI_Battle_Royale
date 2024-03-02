using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class IsInStormCondition : Node
    {
		protected Node childNode;
		private Storm.Storm storm;

		public IsInStormCondition(Node childNode, Storm.Storm storm, ref BlackBoard blackboard) {
			this.childNode = childNode;
			this.storm = storm;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate() {
			BehaviorTree agent;
			blackboard.TryGetValueFromDictionary("agent", out agent);

			Vector3 center = storm.GetCenter();
			float radius = storm.GetRadius();

			float distance = (center - agent.transform.position).magnitude;

			if (distance <= radius)
				nodeState = NodeState.FAILURE;
			else {
				blackboard.SetValueToDictionary("isAnyNodeRunning", false);

				nodeState = NodeState.SUCCESS;
				return nodeState;
			}

			return nodeState;
		}
	}
}
