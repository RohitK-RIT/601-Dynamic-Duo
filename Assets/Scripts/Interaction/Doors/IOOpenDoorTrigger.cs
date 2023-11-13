using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using CharacterController = Core.Player.CharacterController;

namespace Interaction.Doors
{
    public class IOOpenDoorTrigger : IOTaskTrigger
    {
        AudioSource audioSource;

        public GameObject controlDoor;

        public List<GameObject> wireList = new List<GameObject>();
        public Material wireMat;

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

            if(wireList.Count > 0)
            {
                foreach (GameObject go in wireList)
                {
                    Renderer renderer = go.GetComponent<Renderer>();
                    renderer.material = wireMat;
                }
            }

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