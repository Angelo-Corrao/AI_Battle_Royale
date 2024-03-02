using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	[System.Serializable]
	public class Sequence : Node {
		protected List<Node> childNodes = new List<Node>();
		protected List<Node> breakConditions = new List<Node>();

		public Sequence(List<Node> childNodes, ref BlackBoard blackboard, List<Node> breakConditions = null) {
			this.childNodes = childNodes;
			this.blackboard = blackboard;
			if (breakConditions == null)
				this.breakConditions = new List<Node>();
			else
				this.breakConditions = breakConditions;
		}

		public override NodeState Evaluate() {
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool result)) {
				if (result) {
					if (nodeState == NodeState.RUNNING) {
						// Break Conditions
						foreach (Node node in breakConditions) {
							if (node.Evaluate() == NodeState.SUCCESS) {
								nodeState = NodeState.FAILURE;
								return nodeState;
							}
						}

						// Child Evaluation
						foreach (Node node in childNodes) {
							if (node.NodeState == NodeState.RUNNING) {
								nodeState = node.Evaluate();
								return nodeState;
							}
						}
					}
					else {
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
				}
			}

			foreach (Node node in childNodes) {
				switch (node.Evaluate()) {
					case NodeState.RUNNING:
						nodeState = NodeState.RUNNING;
						return nodeState;

					case NodeState.SUCCESS:
						break;

					case NodeState.FAILURE:
						nodeState = NodeState.FAILURE;
						return nodeState;
				}
			}

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
