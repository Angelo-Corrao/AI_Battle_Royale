using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class Reload : Node
	{
		private Inventory.Inventory inventory;

		public Reload(Inventory.Inventory inventory, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.inventory = inventory;
		}

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
				inventory.activeWeapon.Reload(1000);

				nodeState = NodeState.SUCCESS;
				return nodeState;
			}
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
