using DBGA.AI.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DBGA.AI.DamageWrapper
{
	using DBGA.AI.Health;
	public class DamageWrapper : MonoBehaviour
	{
		public IArmor armor;
		public Health health;

		public void ApplyDamage(int damage)
		{
			if (armor != null)
			{
				int exceededDamage;
				exceededDamage = armor.TakeDamage(damage);

				if (exceededDamage > 0)
					health.TakeDamage(exceededDamage);
			}
			else if (health != null)
				health.TakeDamage(damage);
		}

		public void RemoveArmor()
		{
			armor = null;
		}
	}
}
