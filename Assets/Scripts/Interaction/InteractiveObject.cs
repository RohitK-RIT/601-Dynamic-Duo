using System.Collections.Generic;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class InteractiveObject : MonoBehaviour
    {
        protected List<CharacterController> InteractingCharacters { get; private set; }

        public bool Interactable { get; protected set; }

        protected virtual void Start()
        {
            InteractingCharacters = new List<CharacterController>();
            Interactable = true;
        }

        public virtual bool OnInteractionStart(CharacterController controller)
        {
            if (!Interactable) return false;

            InteractingCharacters.Add(controller);
            Debug.Log($"Interaction Started by {controller.name} with {name}");
            return true;
        }

        protected virtual void OnInteractionEnd(bool successful)
        {
            foreach (var interactingCharacter in InteractingCharacters)
                interactingCharacter.OnInteractionEnd(successful);

            InteractingCharacters.Clear();
            Debug.Log("Interaction Ended");
        }
    }
}