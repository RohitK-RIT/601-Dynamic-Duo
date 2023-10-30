using Core.Mini_Games;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    public class MiniGameTrigger : InteractiveObject
    {
        [SerializeField] private MiniGame miniGamePrefabToInstantiate;

        private MiniGame _miniGameInstance;

        public override bool OnInteractionStart(CharacterController controller)
        {
            if (!base.OnInteractionStart(controller) || MiniGame.IsOpen)
                return false;

            _miniGameInstance = Instantiate(miniGamePrefabToInstantiate, Vector3.zero, Quaternion.identity);
            _miniGameInstance.OnClosed += OnMiniGameClosed;
            return true;
        }

        private void OnMiniGameClosed(bool successful)
        {
            if (successful)
                InteractionCompleted();
            else
                OnInteractionEnd();
        }
    }
}