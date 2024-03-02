using DBGA.AI.Sensors;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class GetWeaponCondition : Node
    {
		private PickableSensor pickableSensor;

		public GetWeaponCondition(PickableSensor pickableSensor, ref BlackBoard blackboard) {
			this.pickableSensor = pickableSensor;
			this.blackboard = blackboard;
		}

		public override NodeState Evaluate() {
			List<GameObject> nearWeapons = new List<GameObject>();
			nearWeapons = pickableSensor.GetNearWeapons();

			if (nearWeapons.Count == 0) {
				nodeState = NodeState.FAILURE;
				return nodeState;
			}

			// Calculate nearest weapon
			BehaviorTree agent;
			blackboard.TryGetValueFromDictionary("agent", out agent);
			float nearestDistance = (nearWeapons[0].transform.position - agent.transform.position).sqrMagnitude;
			GameObject nearestWeapon = nearWeapons[0];

			foreach (var weapon in nearWeapons) {
				float distance = (weapon.transform.position - agent.transform.position).sqrMagnitude;
				if (distance < nearestDistance) {
					nearestDistance = distance;
					nearestWeapon = weapon;
				}
			}

			blackboard.SetValueToDictionary("positionToMove", nearestWeapon.transform.position);
			blackboard.SetValueToDictionary("weaponToPick", nearestWeapon);
			blackboard.SetValueToDictionary("isAnyNodeRunning", false);

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
