using System.Collections.Generic;
using UnityEngine;

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
    public abstract class Node
    {
        protected NodeState nodeState;
		protected BlackBoard blackboard;

		public NodeState NodeState { get { return nodeState; } }

        public abstract NodeState Evaluate();
    }
}
