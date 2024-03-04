namespace DBGA.AI.AIs.CorraoAngelo
{
    public abstract class BreakConditions
    {
		protected NodeState nodeState;
		protected BlackBoard blackboard;

		public BreakConditions(ref BlackBoard blackboard)
		{
			this.blackboard = blackboard;
		}

		public NodeState NodeState { get { return nodeState; } }

		public abstract NodeState Evaluate();
	}
}
