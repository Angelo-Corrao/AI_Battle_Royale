using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class AlwaysSucceded : Node
    {
		protected Node childNode;

		public AlwaysSucceded(Node childNode, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.childNode = childNode;
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

			childNode.Evaluate();

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
