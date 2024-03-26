using DBGA.AI.Movement;
using DBGA.AI.Pickable;
using DBGA.AI.Sensors;
using System.Collections.Generic;
using UnityEngine;

namespace DBGA.AI.AIs.CorraoAngelo
{
	using DBGA.AI.DamageWrapper;
	using DBGA.AI.Health;

	public class CorraoAgent : BehaviorTree 
	{
		[SerializeField]
		private DamageWrapper damageWrapper;
		public Storm.Storm storm;
		private PlayerMovement playerMovement;
		private Inventory.Inventory inventory;
		private Picker picker;

		private EyesSensor eyeSensor;
		private PickableSensor pickableSensor;

		private void Awake() 
		{
			if(TryGetComponent<Health>(out Health health))
				damageWrapper.health = health;

			storm = FindObjectOfType<Storm.Storm>();
			playerMovement = GetComponent<PlayerMovement>();
			inventory = GetComponent<Inventory.Inventory>();
			picker = GetComponent<Picker>();

			eyeSensor = GetComponent<EyesSensor>();
			pickableSensor = GetComponent<PickableSensor>();
		}

        protected override Node SetUpTree()
		{
			// Leaves
			var aim = new Aim(playerMovement, ref blackboard);
			var getTarget = new GetTarget(eyeSensor, playerMovement, ref blackboard);
			var getWeapon = new GetWeapon(pickableSensor, ref blackboard);
			var getArmor = new GetArmor(pickableSensor, ref blackboard);
			var moveToPosition = new MoveToPosition(playerMovement, ref blackboard);
			var checkIsInTheStorm = new CheckIsInTheStorm(storm, ref blackboard);
			var getRandomSafePosition = new GetRandomSafePosition(storm, playerMovement, ref blackboard);
			var pickWeapon = new PickWeapon(picker, ref blackboard);
			var pickArmor = new PickArmor(picker, ref blackboard);
			var reload = new Reload(inventory, ref blackboard);
			var shoot = new Shoot(inventory, ref blackboard);

			// Break Conditions
			var stormCondition = new IsInStormCondition(moveToPosition, storm, ref blackboard);
			var targetCondition = new GetTargetCondition(eyeSensor, ref blackboard);
			var weaponCondition = new GetWeaponCondition(pickableSensor, inventory, ref blackboard);
			var armorCondition = new GetArmorCondition(pickableSensor, inventory,  ref blackboard);

			var outOfAmmoReload = new OutOfAmmo(reload, inventory, ref blackboard);
			var alwaysSucceedOutOfAmmo = new AlwaysSucceded(outOfAmmoReload, ref blackboard);
			var getWeaponSequence = new Sequence(new List<Node> { getWeapon, moveToPosition, pickWeapon }, ref blackboard);
			var getArmorSequence = new Sequence(new List<Node> { getArmor, moveToPosition, pickArmor }, ref blackboard);
			var targetSequence = new Sequence(new List<Node> { getTarget, aim, shoot }, ref blackboard);
			var stormSequence = new Sequence(new List<Node> { checkIsInTheStorm, getRandomSafePosition, moveToPosition }, ref blackboard, new List<BreakConditions> { targetCondition });

			var hasWeapon = new HasWeapon(targetSequence, inventory, ref blackboard);
			var hasNoWeapon = new HasNoWeapon(getWeaponSequence, inventory, ref blackboard);
			var hasNoArmor = new HasNoArmor(getArmorSequence, inventory, ref blackboard);
			var wanderSequence = new Sequence(new List<Node> { alwaysSucceedOutOfAmmo, getRandomSafePosition, moveToPosition }, ref blackboard, new List<BreakConditions> { weaponCondition, armorCondition, targetCondition });
			var neutralSelector = new Selector(new List<Node> { hasNoWeapon, hasNoArmor, wanderSequence }, ref blackboard, new List<BreakConditions> { stormCondition });

			return new Selector(new List<Node> { stormSequence, hasWeapon, neutralSelector }, ref blackboard);
		}

		protected override void SetUpBlackboard() 
		{
			blackboard.SetValueToDictionary("agent", this);
			blackboard.SetValueToDictionary("isAnyNodeRunning", false);
			blackboard.SetValueToDictionary("dirToLook", Vector2.zero);
		}
	}
}
