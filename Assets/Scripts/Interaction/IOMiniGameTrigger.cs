using Mini_Games;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using System.Linq;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    public class IOMiniGameTrigger : IOTaskTrigger
    {
        [SerializeField] private MiniGame miniGamePrefabToInstantiate;
        [SerializeField] private List<IOTaskTrigger> conditionalTasks;

        [FormerlySerializedAs("gameObjectToDestroy")] [SerializeField]
        private GameObject doorToDestroy;

        public override bool Interactable => !IsCompleted && (!conditionalTasks.Any() || conditionalTasks.All(trigger => trigger.IsCompleted));

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
            if (successful)
            {
                _miniGameInstance.OnClosed -= OnInteractionEnd;
                Interactable = false;

                if (wireList.Count > 0)
                {
                    foreach (var wireRenderer in wireList.Select(go => go.GetComponent<Renderer>()))
                    {
                        wireRenderer.material = wireMat;
                    }
                }

                if (lightList.Count > 0)
                {
                    foreach (var lightSource in lightList.Select(go => go.GetComponent<Light>()))
                    {
                        lightSource.enabled = true;
                    }
                }

                if (doorToDestroy)
                    Destroy(doorToDestroy);
            }

            base.OnInteractionEnd(successful);
        }
    }
}