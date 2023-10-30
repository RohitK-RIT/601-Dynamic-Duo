using System;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class InteractiveObject : MonoBehaviour
    {
        private CharacterController _currentInteractingCharacter;
        public event Action OnInteractionCompleted;
        public virtual bool OnInteractionStart(CharacterController controller)
        {
            if (_currentInteractingCharacter)
                return false;

            _currentInteractingCharacter = controller;
            Debug.Log($"Interaction Started by {controller.name} with {name}");
            return true;
        }

        protected void InteractionCompleted()
        {
            OnInteractionCompleted?.Invoke();
        }

        public virtual void OnInteractionEnd()
        {
            _currentInteractingCharacter = null;
            Debug.Log("Interaction Ended");
        }
    }
}