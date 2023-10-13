using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public class InteractiveObject : MonoBehaviour
    {
        public bool OnInteractionStart(CharacterController controller)
        {
            Debug.Log($"Interaction Started by {controller.name} with {name}");
            return true;
        }

        public void OnInteractionEnd()
        {
            Debug.Log("Interaction Ended");
        }
    }
}