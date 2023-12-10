using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using CharacterController = Core.Player.CharacterController;

namespace Interaction.Doors
{
    public class IOLightSwitch : IOTaskTrigger
    {
        AudioSource audioSource;

        public GameObject controlDoor;


        //Physical wires
        [Header("Controlled Wires")] public List<GameObject> wireList = new List<GameObject>();
        public Material wireMat;

        [Space(10)]
        //Lights to control
        [Header("Controlled Lights")]
        public List<GameObject> lightList = new List<GameObject>();

        [Space(10)] [Header("Controlled Power Switches")]
        public Material switchMat;

        public GameObject switchLight;

        [Space(10)] [Header("Controlled the availability of other game objects")]
        public GameObject controlledObject;

        protected override void Start()
        {
            base.Start();
            audioSource = GetComponent<AudioSource>();

            //Make the controlled Objects unavailable
            if (controlledObject)
            {
                controlledObject.SetActive(false);
            }
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


            //Control wires
            if (wireList.Count > 0)
            {
                foreach (GameObject go in wireList)
                {
                    Renderer renderer = go.GetComponent<Renderer>();
                    renderer.material = wireMat;
                }
            }

            //Control lights
            if (lightList.Count > 0)
            {
                foreach (GameObject go in lightList)
                {
                    Light light = go.GetComponent<Light>();
                    light.enabled = true;
                }
            }

            //Control switch light
            if (switchLight)
            {
                Renderer renderer = switchLight.GetComponent<Renderer>();
                renderer.material = switchMat;
            }

            //Control other game objects' availability
            if (controlledObject)
            {
                controlledObject.SetActive(true);
            }

            if (controlDoor)
            {
                Destroy(controlDoor);
            }

            if (audioSource)
            {
                audioSource.Play();
            }
            
            OnInteractionEnd(true);
        }
    }
}