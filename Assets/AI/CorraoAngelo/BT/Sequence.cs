using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
	[System.Serializable]
	public class Sequence : Node
	{
		protected List<Node> childNodes = new List<Node>();

		public Sequence(List<Node> childNodes, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.childNodes = childNodes;
		}

		public override NodeState Evaluate()
		{
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool isAnyNodeRunning))
			{
				if (isAnyNodeRunning) {
					if (nodeState == NodeState.RUNNING)
					{
						// Break Conditions
						foreach (BreakConditions breakConditions in breakConditions)
						{
							if (breakConditions.Evaluate() == NodeState.SUCCESS)
							{
								nodeState = NodeState.FAILURE;
								return nodeState;
							}
						}

						// Child Evaluation
						foreach (Node node in childNodes)
						{
							if (node.NodeState == NodeState.RUNNING)
							{
								nodeState = node.Evaluate();
								return nodeState;
							}
						}
					}
					else
					{
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
				}
			}

			foreach (Node node in childNodes)
			{
				switch (node.Evaluate())
				{
					case NodeState.RUNNING:
						nodeState = NodeState.RUNNING;
						return nodeState;

					case NodeState.SUCCESS:
						break;

					case NodeState.FAILURE:
						nodeState = NodeState.FAILURE;
						return nodeState;
				}
			}

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
