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


        //Physical wires
        public List<GameObject> wireList = new List<GameObject>();
        public Material wireMat;

        //Lights to control
        public List<GameObject> lightList = new List<GameObject>();


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

            if (lightList.Count > 0)
            {
                foreach (GameObject go in wireList)
                {
                    Light light = go.GetComponent<Light>();
                    light.enabled = true;
                }
            }

            if (controlDoor)
            {
                Destroy(controlDoor);
            }
            
            if(audioSource)
            {
                audioSource.Play();
            }
            
        }

        protected override void OnInteractionEnd(bool successful)
        {
            base.OnInteractionEnd(successful);
            Interactable = !successful;
        }
    }
}