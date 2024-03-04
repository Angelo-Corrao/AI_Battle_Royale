using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class IsRifleEquipped : Node
    {
		protected Node childNode;
		private Inventory.Inventory inventory;

		public IsRifleEquipped(Node childNode, Inventory.Inventory inventory, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.childNode = childNode;
			this.inventory = inventory;
		}

		public override NodeState Evaluate()
		{
			// MISSING FEATURE
			// if inventory active weapon is the rifle
			//nodeState = NodeState.SUCCESS;

			// else
			nodeState = NodeState.FAILURE;

			return nodeState;
		}
	}
}
