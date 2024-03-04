using DBGA.AI.Pickable;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class PickArmor : Node
	{
		private Picker picker;

		public PickArmor(Picker picker, ref BlackBoard blackboard, List<BreakConditions> breakConditions = null)
			: base(ref blackboard, breakConditions)
		{
			this.picker = picker;
		}

		public override NodeState Evaluate()
		{
			NodeState parentState = base.Evaluate();

			if (parentState == NodeState.SUCCESS)
			{
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
			else if (parentState == NodeState.FAILURE)
				return NodeState.FAILURE;
			else
				return NodeState.DEFAULT;
		}
	}
}
