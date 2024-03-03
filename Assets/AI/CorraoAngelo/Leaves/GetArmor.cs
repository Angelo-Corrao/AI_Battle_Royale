using DBGA.AI.Sensors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class GetArmor : Node
	{
		private PickableSensor pickableSensor;

		public GetArmor(PickableSensor pickableSensor, ref BlackBoard blackboard)
		{
			this.pickableSensor = pickableSensor;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate()
		{
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool isAnyNodeRunning))
			{
				if (isAnyNodeRunning)
				{
					if (nodeState != NodeState.RUNNING)
					{
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
				}
			}

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
	}
}
