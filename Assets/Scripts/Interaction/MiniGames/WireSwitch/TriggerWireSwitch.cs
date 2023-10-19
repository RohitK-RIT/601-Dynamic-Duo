using System.Collections;
using System.Collections.Generic;
using CharacterController = Core.Player.CharacterController;
using UnityEngine;


namespace Interaction
{
    public class TriggerWireSwitch : InteractiveObject
    {
        public GameObject prefabToInstantiate;

        public override bool OnInteractionStart(CharacterController controller)
        {
            Debug.Log($"{controller.name} start mini game wire switch!");
            GameObject newPrefabInstance = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
            return true;
        }

        public override void OnInteractionEnd()
        {
            Debug.Log("Interaction Ended");
        }
    }
}
