using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class GetArmor : Node
	{
		private PickableSensor pickableSensor;

		public GetArmor(PickableSensor pickableSensor, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null) 
			: base(ref blackboard, breakConditions)
		{
			this.pickableSensor = pickableSensor;
		}

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
				List<GameObject> nearArmors = new List<GameObject>();
				nearArmors = pickableSensor.GetNearArmors();

				if (nearArmors.Count == 0)
				{
					nodeState = NodeState.FAILURE;
					return nodeState;
				}

				// Calculate nearest armor
				BehaviorTree agent;
				blackboard.TryGetValueFromDictionary("agent", out agent);
				float nearestDistance = (nearArmors[0].transform.position - agent.transform.position).sqrMagnitude;
				GameObject nearestArmor = nearArmors[0];

				foreach (var armor in nearArmors)
				{
					float distance = (armor.transform.position - agent.transform.position).sqrMagnitude;
					if (distance < nearestDistance)
					{
						nearestDistance = distance;
						nearestArmor = armor;
					}
				}

				blackboard.SetValueToDictionary("positionToMove", nearestArmor.transform.position);
				blackboard.SetValueToDictionary("armorToPick", nearestArmor);
			
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
