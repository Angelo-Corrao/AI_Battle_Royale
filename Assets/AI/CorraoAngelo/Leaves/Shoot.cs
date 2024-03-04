using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class Shoot : Node {
		private Inventory.Inventory inventory;

		public Shoot(Inventory.Inventory inventory, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.inventory = inventory;
		}

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
				if (!inventory || inventory.activeWeapon == null)
				{
					blackboard.SetValueToDictionary("isAnyNodeRunning", false);
					return NodeState.FAILURE;
				}	

				inventory.activeWeapon.Shoot();
				blackboard.SetValueToDictionary("isAnyNodeRunning", false);

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
