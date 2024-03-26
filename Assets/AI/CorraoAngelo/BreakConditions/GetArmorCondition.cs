using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	public class GetArmorCondition : BreakConditions
	{
		private PickableSensor pickableSensor;
		private Inventory.Inventory inventory;

		public GetArmorCondition(PickableSensor pickableSensor, Inventory.Inventory inventory, ref BlackBoard blackboard)
			: base(ref blackboard)
		{
			this.pickableSensor = pickableSensor;
			this.inventory = inventory;
		}

		public override NodeState Evaluate()
		{
			if (inventory.armor != null)
			{
				nodeState = NodeState.FAILURE;
				return nodeState;
			}

			List<GameObject> nearArmors = new List<GameObject>();
			nearArmors = pickableSensor.GetNearArmors();

			if (nearArmors.Count == 0)
			{
				nodeState = NodeState.FAILURE;
				return nodeState;
			}

			blackboard.SetValueToDictionary("isAnyNodeRunning", false);
			nodeState = NodeState.SUCCESS;
			return nodeState;
		}
	}
}
