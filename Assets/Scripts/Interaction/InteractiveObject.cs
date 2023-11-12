using System;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class InteractiveObject : MonoBehaviour
    {
        protected List<CharacterController> CurrentInteractingCharacters { get; private set; }
        protected List<CharacterController> PlayersInRange { get; private set; }
        public event Action<bool> OnInteractionCompleted;

        private void Start()
        {
            CurrentInteractingCharacters = new List<CharacterController>();
            PlayersInRange = new List<CharacterController>();
        }

        public virtual bool OnInteractionStart(CharacterController controller)
        {
            if (PlayersInRange.Count <= 0)
                return false;

            CurrentInteractingCharacters.Add(controller);
            Debug.Log($"Interaction Started by {controller.name} with {name}");
            return true;
        }

        protected virtual void OnInteractionEnd(bool successful)
        {
            OnInteractionCompleted?.Invoke(successful);
            CurrentInteractingCharacters.Clear();
            Debug.Log("Interaction Ended");
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<CharacterController>();
            if (player)
            {
                PlayersInRange.Add(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<CharacterController>();
            if (player)
            {
                PlayersInRange.Remove(player);
            }
        }
    }
}