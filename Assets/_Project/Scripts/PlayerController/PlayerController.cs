using DBGA.AI.Common;
using DBGA.AI.Pickable;
using DBGA.AI.Movement;
using UnityEngine;

namespace DBGA.AI.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private KeyCode shootKey;
        [SerializeField]
        private KeyCode swapWeaponKey;
        [SerializeField]
        private KeyCode pickObjectKey;

        private PlayerInputs playerInputs;
        private PlayerMovement playerMovement;
        private Vector2 moveDirection;

        private IInventory inventory;
        private Picker picker;

        void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();

            playerInputs = new PlayerInputs();
            playerInputs.Character.Move.performed += _ => moveDirection = playerInputs.Character.Move.ReadValue<Vector2>();
            playerInputs.Character.Move.canceled += _ => moveDirection = Vector2.zero;

            inventory = GetComponent<IInventory>();
            picker = GetComponent<Picker>();
        }

        void OnEnable()
        {
            playerInputs.Enable();
        }

        void OnDisable()
        {
            playerInputs.Disable();
        }

        void Update()
        {
            playerMovement.MoveToward(moveDirection);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                Vector2 aimDirection = new(hit.point.x, hit.point.z);
                playerMovement.SetDirection(aimDirection);
            }

            if (Input.GetKey(shootKey))
                inventory.GetActiveWeapon()?.Shoot();
            if (Input.GetKeyDown(swapWeaponKey))
                inventory.SwapWeapons();
            if (Input.GetKeyDown(pickObjectKey))
                picker.Pick();
        }
    }
}
