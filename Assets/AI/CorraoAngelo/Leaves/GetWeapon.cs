using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class GetWeapon : Node
	{
		private PickableSensor pickableSensor;

		public GetWeapon(PickableSensor pickableSensor, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null) 
			: base(ref blackboard, breakConditions) 
		{
			this.pickableSensor = pickableSensor;
		}

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
				List<GameObject> nearWeapons = new List<GameObject>();
				nearWeapons = pickableSensor.GetNearWeapons();
			
				if (nearWeapons.Count == 0)
				{
					nodeState = NodeState.FAILURE;
					return nodeState;
				}

				// Calculate nearest weapon
				BehaviorTree agent;
				blackboard.TryGetValueFromDictionary("agent", out agent);
				float nearestDistance = (nearWeapons[0].transform.position - agent.transform.position).sqrMagnitude;
				GameObject nearestWeapon = nearWeapons[0];

				foreach (var weapon in nearWeapons)
				{
					float distance = (weapon.transform.position - agent.transform.position).sqrMagnitude;
					if (distance < nearestDistance)
					{
						nearestDistance = distance;
						nearestWeapon = weapon;
					}
				}

				blackboard.SetValueToDictionary("positionToMove", nearestWeapon.transform.position);
				blackboard.SetValueToDictionary("weaponToPick", nearestWeapon);

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
