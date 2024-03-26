using DBGA.AI.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class MoveToPosition : Node
    {
		private PlayerMovement playerMovement;
		private Vector3 positionToMove;
		private Vector2 directionToMove = Vector2.zero;

		public MoveToPosition(PlayerMovement playerMovement, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.playerMovement = playerMovement;
		}

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
				BehaviorTree agent;
				blackboard.TryGetValueFromDictionary("agent", out agent);
				blackboard.TryGetValueFromDictionary("positionToMove", out positionToMove);
				// Doesn't count the y of the agent
				float actualDistance = (positionToMove - agent.transform.position).sqrMagnitude - agent.transform.position.y;

				if (actualDistance > 1.5f)
				{
					playerMovement.MoveToward(positionToMove);
					playerMovement.Rotate();

					blackboard.SetValueToDictionary("isAnyNodeRunning", true);
					nodeState = NodeState.RUNNING;
					return nodeState;
				}
				else
				{
					blackboard.SetValueToDictionary("isAnyNodeRunning", false);
					directionToMove = Vector2.zero;
					nodeState = NodeState.SUCCESS;
					return nodeState;
				}
			}
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
