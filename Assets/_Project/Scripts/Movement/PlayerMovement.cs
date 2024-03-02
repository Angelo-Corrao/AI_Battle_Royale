using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Movement
{
    public class PlayerMovement : MonoBehaviour, IMover
    {
        public float speed = 10;
        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void MoveToward(Vector2 direction)
        {
            characterController.Move(new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime);
        }

        public void SetDirection(Vector2 direction)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        }
    }
}
