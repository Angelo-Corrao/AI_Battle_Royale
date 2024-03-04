using DBGA.AI.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class AimToDirection : Node
    {
		private PlayerMovement playerMovement;

		public AimToDirection(PlayerMovement playerMovement, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null) 
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

				if (blackboard.TryGetValueFromDictionary("targetEnemy", out GameObject enemy)) {
					Vector3 direction = (enemy.transform.position - agent.transform.position).normalized;
					playerMovement.SetDirection(new Vector2(direction.x, direction.z));

					nodeState = NodeState.SUCCESS;
				}
				else
					nodeState = NodeState.FAILURE;

				return nodeState;
			}
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
