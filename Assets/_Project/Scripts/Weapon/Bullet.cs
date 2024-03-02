using UnityEngine;

namespace DBGA.AI.Weapon
{
	public class Bullet : MonoBehaviour
	{
		internal float speed;
		internal int damage;
		internal float lifeTime;

		void Start()
		{
			Destroy(gameObject, lifeTime);
		}

		void Update()
		{
			transform.position += transform.forward * speed * Time.deltaTime;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				other.GetComponent<DamageWrapper.DamageWrapper>().ApplyDamage(damage);

			if (!other.CompareTag("Weapon") && !other.CompareTag("Armor"))
				Destroy(gameObject);
		}
	}
}
