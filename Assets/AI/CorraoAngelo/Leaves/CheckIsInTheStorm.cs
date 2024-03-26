using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class CheckIsInTheStorm : Node
    {
		private Storm.Storm storm;

		public CheckIsInTheStorm(Storm.Storm storm, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.storm = storm;
		}

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
				BehaviorTree agent;
				blackboard.TryGetValueFromDictionary("agent", out agent);

				Vector3 center = storm.GetCenter();
				float radius = storm.GetRadius();

				float distance = (center - agent.transform.position).magnitude;

				if (distance <= radius)
					nodeState = NodeState.FAILURE;
				else
				{
					nodeState = NodeState.SUCCESS;
				}

				return nodeState;
			}
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
