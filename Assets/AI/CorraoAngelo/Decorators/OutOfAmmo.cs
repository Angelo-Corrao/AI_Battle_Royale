using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class OutOfAmmo : Node {
		protected Node childNode;
		Inventory.Inventory inventory;

		public OutOfAmmo(Node childNode, Inventory.Inventory inventory, ref BlackBoard blackboard) {
			this.childNode = childNode;
			this.inventory = inventory;
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

			// MISSING FEATURE
			nodeState = NodeState.FAILURE;
			return nodeState;
		}
	}
}
