using Mini_Games;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    public class MiniGameTrigger : InteractiveObject
    {
        [SerializeField] private MiniGame miniGamePrefabToInstantiate;
        [FormerlySerializedAs("gameObjectToDestroy")] [SerializeField] private GameObject doorToDestroy;

        public bool IsMiniGameCompleted { get; private set; }

        private MiniGame _miniGameInstance;

        public override bool OnInteractionStart(CharacterController controller)
        {
            if (!base.OnInteractionStart(controller) || MiniGame.IsOpen)
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
            IsMiniGameCompleted = successful;
            Interactable = !successful;
            
            if(doorToDestroy)
                Destroy(doorToDestroy);
            
            base.OnInteractionEnd(successful);
        }
    }
}