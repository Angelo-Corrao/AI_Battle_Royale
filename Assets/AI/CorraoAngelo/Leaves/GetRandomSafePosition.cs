using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class GetRandomSafePosition : Node
    {
		private Storm.Storm storm;

		public GetRandomSafePosition(Storm.Storm storm, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
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

				Vector2 randomDirection = Random.insideUnitCircle.normalized;
				float randomLenght = Random.Range(1, radius);
				Vector3 randomPointInsideSafe = center + new Vector3(randomDirection.x, 0, randomDirection.y) * randomLenght;
				Vector3 dirToMove = (randomPointInsideSafe - agent.transform.position).normalized;
				Vector2 dirToLook = new Vector2(dirToMove.x, dirToMove.z);

				blackboard.SetValueToDictionary("dirToLook", dirToLook);
				blackboard.SetValueToDictionary("positionToMove", randomPointInsideSafe);

				nodeState = NodeState.SUCCESS;
				return nodeState;
			}
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
