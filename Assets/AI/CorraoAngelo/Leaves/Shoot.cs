using DBGA.AI.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class Shoot : Node {
		private Inventory.Inventory inventory;

		public Shoot(Inventory.Inventory inventory, ref BlackBoard blackboard) {
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

			if(!inventory || inventory.activeWeapon == null)
			{
				return NodeState.FAILURE;
			}	

			inventory.activeWeapon.Shoot();

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
