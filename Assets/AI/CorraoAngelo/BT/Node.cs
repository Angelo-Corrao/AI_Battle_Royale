using System.Collections.Generic;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public enum NodeState
    {
        DEFAULT,
        RUNNING,
        SUCCESS,
        FAILURE
    }

    [System.Serializable]
    public class Node
    {
        protected NodeState nodeState;
		protected BlackBoard blackboard;
		protected List<BreakConditions> breakConditions = new List<BreakConditions>();

        public Node(ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
        {
			this.blackboard = blackboard;

			if (breakConditions == null)
				this.breakConditions = new List<BreakConditions>();
			else
				this.breakConditions = breakConditions;
		}

		public NodeState NodeState { get { return nodeState; } }

        public virtual NodeState Evaluate() 
        {
			// Check if this is the current running node
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool isAnyNodeRunning))
			{
				if (isAnyNodeRunning)
				{
					if (nodeState != NodeState.RUNNING)
					{
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
					else
					{
						// Break Conditions
						foreach (BreakConditions breakConditions in breakConditions)
						{
							if (breakConditions.Evaluate() == NodeState.SUCCESS)
							{
								blackboard.SetValueToDictionary("isAnyNodeRunning", false);
								nodeState = NodeState.FAILURE;
								return nodeState;
							}
						}
					}
				}
			}

			return NodeState.SUCCESS;
		}
    }
}
