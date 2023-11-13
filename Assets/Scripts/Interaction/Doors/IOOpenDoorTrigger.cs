using System.Collections;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction.Doors
{
    public class IOOpenDoorTrigger : IOTaskTrigger
    {
        AudioSource audioSource;

        public GameObject controlDoor;

        protected override void Start()
        {
            base.Start();
            audioSource = GetComponent<AudioSource>();
        }

        public override bool OnHandleInteractee(CharacterController controller)
        {
            if (!base.OnHandleInteractee(controller))
                return false;

            StartCoroutine(DoorOpened());
            return true;
        }

        private IEnumerator DoorOpened()
        {
            yield return null;

            OnInteractionEnd(true);
            Destroy(controlDoor);
            audioSource.Play();
        }

        protected override void OnInteractionEnd(bool successful)
        {
            base.OnInteractionEnd(successful);
            Interactable = !successful;
        }
    }
}