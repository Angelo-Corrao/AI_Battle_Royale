using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class OutOfAmmo : Node {
		protected Node childNode;
		Inventory.Inventory inventory;

		public OutOfAmmo(Node childNode, Inventory.Inventory inventory, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
		: base(ref blackboard, breakConditions)
		{
			this.childNode = childNode;
			this.inventory = inventory;
		}

		public override NodeState Evaluate()
		{
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
