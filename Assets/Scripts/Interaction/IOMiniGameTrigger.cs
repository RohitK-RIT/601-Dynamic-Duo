using Mini_Games;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    public class IOMiniGameTrigger : IOTaskTrigger
    {
        [SerializeField] private MiniGame miniGamePrefabToInstantiate;

        [FormerlySerializedAs("gameObjectToDestroy")] [SerializeField]
        private GameObject doorToDestroy;


        private MiniGame _miniGameInstance;


        //Physical wires
        public List<GameObject> wireList = new List<GameObject>();
        public Material wireMat;

        //Lights to control
        public List<GameObject> lightList = new List<GameObject>();

        public override bool OnHandleInteractee(CharacterController controller)
        {
            if (!base.OnHandleInteractee(controller) || MiniGame.IsOpen)
                return false;

            if (InteractingCharacters.Count >= 2)
            {
                _miniGameInstance = Instantiate(miniGamePrefabToInstantiate, Vector3.zero, Quaternion.identity);
                _miniGameInstance.OnClosed += OnInteractionEnd;
            }

            return true;
        }

        protected override void OnInteractionEnd(bool successful)
        {
            _miniGameInstance.OnClosed -= OnInteractionEnd;
            Interactable = !successful;

            if (wireList.Count > 0)
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

            if (doorToDestroy)
                Destroy(doorToDestroy);

            base.OnInteractionEnd(successful);
        }
    }
}