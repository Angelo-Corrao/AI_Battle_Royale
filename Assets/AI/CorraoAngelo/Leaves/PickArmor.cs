using DBGA.AI.Pickable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class PickArmor : Node
	{
		private Picker picker;

		public PickArmor(Picker picker, ref BlackBoard blackboard)
		{
			this.picker = picker;
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

			if (blackboard.TryGetValueFromDictionary("armorToPick", out GameObject armorObject))
			{
				if (armorObject.TryGetComponent<ArmorPicker>(out ArmorPicker armorPicker))
				{
					BehaviorTree agent;
					blackboard.TryGetValueFromDictionary("agent", out agent);
					armorPicker.Interact(agent.gameObject);
				}
			}

			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
