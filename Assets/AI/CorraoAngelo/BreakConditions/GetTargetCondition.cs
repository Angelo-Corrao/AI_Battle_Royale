using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
    public class GetTargetCondition : BreakConditions
    {
		private EyesSensor eyeSensor;

		public GetTargetCondition(EyesSensor eyeSensor, ref BlackBoard blackboard)
			: base(ref blackboard)
		{
			this.eyeSensor = eyeSensor;
		}

		public override NodeState Evaluate()
		{
			List<GameObject> enemies = new List<GameObject>();
			enemies = eyeSensor.GetEnemiesTargets();

			if (enemies.Count == 0) 
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
