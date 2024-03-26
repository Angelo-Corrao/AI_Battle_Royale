using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class Inverter : Node
    {
		protected Node childNode;

		public Inverter(Node childNode, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
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

			switch (childNode.Evaluate())
			{
				case NodeState.RUNNING:
					nodeState = NodeState.RUNNING;
					break;

				case NodeState.SUCCESS:
					nodeState = NodeState.FAILURE;
					break;

				case NodeState.FAILURE:
					nodeState = NodeState.SUCCESS;
					break;
			}

			return nodeState;
		}
	}
}
