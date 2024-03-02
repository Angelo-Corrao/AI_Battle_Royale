using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class Reload : Node {
		private Inventory.Inventory inventory;

		public Reload(Inventory.Inventory inventory, ref BlackBoard blackboard) {
			this.inventory = inventory;
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

			inventory.activeWeapon.Reload(1000);

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
