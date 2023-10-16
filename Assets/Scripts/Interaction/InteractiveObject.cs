using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class InteractiveObject : MonoBehaviour
    {
        private CharacterController _currentInteractingCharacter;
        public virtual bool OnInteractionStart(CharacterController controller)
        {
            if (_currentInteractingCharacter)
                return false;

            _currentInteractingCharacter = controller;
            Debug.Log($"Interaction Started by {controller.name} with {name}");
            return true;
        }

        public virtual void OnInteractionEnd()
        {
            _currentInteractingCharacter = null;
            Debug.Log("Interaction Ended");
        }
    }
}