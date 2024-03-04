using DBGA.AI.Pickable;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class PickWeapon : Node
    {
		private Picker picker;

        public PickWeapon(Picker picker, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
            this.picker = picker;
        }

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
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
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
