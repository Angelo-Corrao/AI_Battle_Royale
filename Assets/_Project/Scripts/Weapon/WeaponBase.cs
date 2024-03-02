using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Weapon
{
    public class WeaponBase : MonoBehaviour, IWeapon
    {
        [Header("Weapon stats")]
        [SerializeField]
        private int maxAmmo;
        [SerializeField]
        private float fireRate;
        [SerializeField]
        private int damage;

        [Header("Bullets stats")]
        [SerializeField]
        private float bulletLifetime;
        [SerializeField]
        private float bulletSpeed;
        [Tooltip("the size to multiplicate to the bullet - Base = 1")]
        [SerializeField]
        private float bulletSize = 1;

        [Header("Weapon components")]
        [Tooltip("the mesh of this weapon")]
        [SerializeField]
        private Renderer mesh;
        [SerializeField]
        [Tooltip("the shot holes of this weapons")]
        private Transform[] shotTransform;
        [SerializeField]
        private Bullet bullet;

        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Collider weaponCollider;
        [SerializeField]
        private Collider weaponTrigger;

        [SerializeField]
        private int currentAmmo;
        private float fireRateTimer = 0;

        void Start()
        {
            currentAmmo = maxAmmo;
        }

        void Update()
        {
            fireRateTimer = Mathf.Max(fireRateTimer - Time.deltaTime, 0);
        }

        public void Disable()
        {
            mesh.enabled = false;
        }

        public void Enable()
        {
            mesh.enabled = true;
        }

        public void Reload(int ammoCount)
        {
            currentAmmo = Mathf.Min((currentAmmo + ammoCount), maxAmmo);
        }

        public void Shoot()
        {
            if (fireRateTimer > 0 || currentAmmo <= 0)
                return;

            fireRateTimer = fireRate;

            foreach (Transform shot in shotTransform)
            {
                Bullet nbullet = Instantiate(bullet, shot.position, transform.rotation);
                nbullet.damage = this.damage;
                nbullet.lifeTime = this.bulletLifetime;
                nbullet.speed = this.bulletSpeed;
                nbullet.transform.localScale *= this.bulletSize;
                currentAmmo--;
            }
        }

        public bool IsOutOfAmmo()
        {
            return currentAmmo == 0;
        }

        public int GetCurrentAmmo()
        {
            return currentAmmo;
        }

        public int GetMaxAmmo()
        {
            return maxAmmo;
        }

		public float GetFireRate()
        {
            return fireRate;
        }

        public int GetDamage()
        {
            return damage;
        }

        public void Drop()
        {
            transform.position += Vector3.up + Vector3.forward;
            //rb.useGravity = true;
            gameObject.layer = LayerMask.GetMask("Pickable");
            weaponCollider.enabled = true;
            weaponTrigger.enabled = true;
            transform.SetParent(null);
        }
    }
}
