using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class Inverter : Node
    {
		protected Node childNode;

		public Inverter(Node childNode, ref BlackBoard blackboard) {
			this.childNode = childNode;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate() {
			switch (childNode.Evaluate()) {
				case NodeState.RUNNING:
					nodeState = NodeState.RUNNING;
					break;

				case NodeState.SUCCESS:
					nodeState = NodeState.FAILURE;
					break;

				case NodeState.FAILURE:
					nodeState = NodeState.SUCCESS;
					break;
			}

			return nodeState;
		}
	}
}
