using DBGA.AI.Sensors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class IsRifleEquipped : Node
    {
		protected Node childNode;
		private Inventory.Inventory inventory;

		public IsRifleEquipped(Node childNode, Inventory.Inventory inventory, ref BlackBoard blackboard) {
			this.childNode = childNode;
			this.inventory = inventory;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate() {
			// MISSING FEATURE
			// if inventory active weapon is the rifle
			//nodeState = NodeState.SUCCESS;

			// else
			nodeState = NodeState.FAILURE;

			return nodeState;
		}
	}
}
