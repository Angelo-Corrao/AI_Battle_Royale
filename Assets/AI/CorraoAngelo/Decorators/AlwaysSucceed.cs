using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class AlwaysSucceded : Node
    {
		protected Node childNode;

		public AlwaysSucceded(Node childNode, ref BlackBoard blackboard) {
			this.childNode = childNode;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate() {
			childNode.Evaluate();

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
