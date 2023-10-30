using System.Linq;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction.OpenDoor
{
    public class OpenDoorSwitch : InteractiveObject
    {
        [SerializeField] private MiniGameTrigger[] miniGameTriggers;

        [SerializeField] private Material interactableDoor, unInteractableDoor;
        [SerializeField] private Renderer switchRenderer;
        //Testing: change color if door is opened
        [SerializeField] private Renderer doorRenderer;
        [SerializeField] private Material newMaterial;

        private bool Interactable
        {
            get => _interactable;
            set
            {
                switchRenderer.material = value ? interactableDoor : unInteractableDoor;
                _interactable = value;
            }
        }
        private bool _interactable;
        private void Start()
        {
            Interactable = false;
        }

        public override bool OnInteractionStart(CharacterController controller)
        {
            if (!_interactable)
                return false;
            
            if (!base.OnInteractionStart(controller))
                return false;

            Debug.Log($"{controller.name} opened the door!");
            doorRenderer.material = newMaterial;
            Invoke(nameof(InteractionCompleted), 0.1f);
            return true;
        }

        private void Update()
        {
            if (miniGameTriggers.All(trigger => trigger.IsMiniGameCompleted))
                Interactable = true;
        }
    }
}