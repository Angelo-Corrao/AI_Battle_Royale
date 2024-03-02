using DBGA.AI.Sensors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class IsEnemyClose : Node
    {
		protected Node childNode;

		public IsEnemyClose(Node childNode, ref BlackBoard blackboard) {
			this.childNode = childNode;
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

			if (blackboard.TryGetValueFromDictionary("targetEnemy", out GameObject enemy)) {
				float distance = (enemy.transform.position - agent.transform.position).sqrMagnitude;

				if (distance <= Mathf.Pow(2, 2))
					nodeState = NodeState.SUCCESS;
				else
					nodeState = NodeState.FAILURE;
			}

			return nodeState;
		}
	}
}
