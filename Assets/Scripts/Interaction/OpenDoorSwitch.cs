using Interaction;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

public class OpenDoorSwitch : InteractiveObject
{
    public override bool OnInteractionStart(CharacterController controller)
    {
        Debug.Log($"{controller.name} interacted with Door {name}");
        return true;
    }
}
