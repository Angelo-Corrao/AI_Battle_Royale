using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class GetWeaponCondition : BreakConditions
    {
		private PickableSensor pickableSensor;
		private Inventory.Inventory inventory;

		public GetWeaponCondition(PickableSensor pickableSensor, Inventory.Inventory inventory, ref BlackBoard blackboard)
			: base(ref blackboard)
		{
			this.pickableSensor = pickableSensor;
			this.inventory = inventory;
		}

		public override NodeState Evaluate()
		{
			if (inventory.activeWeapon != null)
			{
				nodeState = NodeState.FAILURE;
				return nodeState;
			}
			
			List<GameObject> nearWeapons = new List<GameObject>();
			nearWeapons = pickableSensor.GetNearWeapons();

			if (nearWeapons.Count == 0) 
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
