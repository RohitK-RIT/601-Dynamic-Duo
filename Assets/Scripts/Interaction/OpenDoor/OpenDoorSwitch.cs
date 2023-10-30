using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction.OpenDoor
{
    public class OpenDoorSwitch : InteractiveObject
    {
        //Testing: change color if door is opened
        [SerializeField] private Renderer doorRenderer;
        [SerializeField] private Material newMaterial;

        public override bool OnInteractionStart(CharacterController controller)
        {
            if (!base.OnInteractionStart(controller))
                return false;

            Debug.Log($"{controller.name} opened the door!");
            doorRenderer.material = newMaterial;
            Invoke(nameof(InteractionCompleted), 0.1f);
            return true;
        }
    }
}