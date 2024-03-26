using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class HasNoArmor : Node
	{
		protected Node childNode;
		private Inventory.Inventory inventory;

		public HasNoArmor(Node childNode, Inventory.Inventory inventory, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.childNode = childNode;
			this.inventory = inventory;
		}

		public override NodeState Evaluate()
		{
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool result))
			{
				if (result)
				{
					if (nodeState == NodeState.RUNNING)
					{
						nodeState = childNode.Evaluate();
						return nodeState;
					}
					else
					{
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
				}
			}

			if (inventory.armor == null)
			{
				nodeState = childNode.Evaluate();
				return nodeState;
			}

			nodeState = NodeState.FAILURE;
			return nodeState;
		}
	}
}
