using System.Linq;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction.OpenDoor
{
    public class OpenDoorSwitch : InteractiveObject
    {
        [SerializeField] private MiniGameTrigger[] miniGameTriggers;

        [SerializeField] private Material interactableDoor, unInteractableDoor;
        //[SerializeField] private Renderer switchRenderer;
        //Testing: change color if door is opened
        [SerializeField] private Renderer doorRenderer;
        [SerializeField] private Material newMaterial;

        AudioSource audioSource;

        public GameObject controlDoor;

        private bool Interactable
        {
            get => _interactable;
            set
            {
                //switchRenderer.material = value ? interactableDoor : unInteractableDoor;
                _interactable = value;
            }
        }
        private bool _interactable;
        private void Start()
        {
            Interactable = false;
            audioSource = GetComponent<AudioSource>();
        }

        public override bool OnInteractionStart(CharacterController controller)
        {
            if (!Interactable)
                return false;
            
            if (!base.OnInteractionStart(controller))
                return false;

            //Debug.Log($"{controller.name} opened the door!");
            Destroy(controlDoor);
            audioSource.Play();
            //Invoke(nameof(InteractionCompleted), 0.1f);
            return false;
        }

        private void Update()
        {
            if (miniGameTriggers.All(trigger => trigger.IsMiniGameCompleted))
                Interactable = true;
        }
    }
}