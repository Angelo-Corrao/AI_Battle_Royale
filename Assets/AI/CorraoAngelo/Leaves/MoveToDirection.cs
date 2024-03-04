using DBGA.AI.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class MoveToDirection : Node {
		private PlayerMovement playerMovement;
		private float totMoveTime;
		private float time = 0;
		private Vector2 randomDirection;

		public MoveToDirection(PlayerMovement playerMovement, float totMoveTime, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null) 
			: base(ref blackboard, breakConditions)
		{
			this.playerMovement = playerMovement;
			this.totMoveTime = totMoveTime;
		}

		public override NodeState Evaluate() 
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
				if (nodeState != NodeState.RUNNING)
					randomDirection = Random.insideUnitCircle.normalized;

				if (blackboard.TryGetValueFromDictionary("hasToStop", out bool result)) {
					if (result) {
						blackboard.SetValueToDictionary("hasToStop", false);
						time = 0;

						blackboard.SetValueToDictionary("isAnyNodeRunning", false);
						nodeState = NodeState.FAILURE;
						return nodeState;
					}
				}

				if (time >= totMoveTime) {
					time = 0;

					blackboard.SetValueToDictionary("isAnyNodeRunning", false);
					nodeState = NodeState.SUCCESS;
					return nodeState;
				}
				else {
					time += Time.deltaTime;
					playerMovement.MoveToward(randomDirection);
					playerMovement.SetDirection(randomDirection);
				
					nodeState = NodeState.RUNNING;
					blackboard.SetValueToDictionary("isAnyNodeRunning", true);
					blackboard.SetValueToDictionary("dirToLook", randomDirection);
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
