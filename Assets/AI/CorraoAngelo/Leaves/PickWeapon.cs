using DBGA.AI.Common;
using DBGA.AI.Pickable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class PickWeapon : Node
    {
		private Picker picker;

        public PickWeapon(Picker picker, ref BlackBoard blackboard) {
            this.picker = picker;
            this.blackboard = blackboard;
        }

		public override NodeState Evaluate() {
			if (blackboard.TryGetValueFromDictionary("isAnyNodeRunning", out bool isAnyNodeRunning)) {
				if (isAnyNodeRunning) {
					if (nodeState != NodeState.RUNNING) {
						nodeState = NodeState.DEFAULT;
						return nodeState;
					}
				}
			}

			if (blackboard.TryGetValueFromDictionary("weaponToPick", out GameObject weaponObject))
			{
				if (weaponObject.TryGetComponent<WeaponPicker>(out WeaponPicker weaponPicker))
				{
					BehaviorTree agent;
					blackboard.TryGetValueFromDictionary("agent", out agent);
					weaponPicker.Interact(agent.gameObject);
				}
			}
			
			nodeState = NodeState.SUCCESS;
            return nodeState;
		}
	}
}
