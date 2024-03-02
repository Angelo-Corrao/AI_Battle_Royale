using DBGA.AI.Common;
using UnityEngine;

namespace DBGA.AI.Pickable
{
    /// <summary>
    /// Detects near object and try to pick it.
    /// Pickable items need to be on a layer named "Pickable".
    /// </summary>
    public class Picker : MonoBehaviour
    {
        private IInteractable itemInRange = null;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(LayerDefinition.PICKABLE))
                other.TryGetComponent<IInteractable>(out itemInRange);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(LayerDefinition.PICKABLE))
                itemInRange = null;
        }

        public void Pick()
        {
            if (itemInRange != null)
                itemInRange?.Interact(gameObject);
        }
    }
}
