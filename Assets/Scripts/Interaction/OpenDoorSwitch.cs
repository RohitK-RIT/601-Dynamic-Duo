using Interaction;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

public class OpenDoorSwitch : InteractiveObject
{
    //Testing: change color if door is opened
    public GameObject Door;
    public Material newMaterial;

    public override bool OnInteractionStart(CharacterController controller)
    {
        Renderer renderer = Door.GetComponent<Renderer>();
        renderer.material = newMaterial;

        Debug.Log($"{controller.name} opened the door!");
        return true;
    }
}
