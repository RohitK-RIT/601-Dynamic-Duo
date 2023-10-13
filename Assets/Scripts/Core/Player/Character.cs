using Interaction;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        private CharacterController Controller { get; set; }

        [SerializeField] private float speed = 10f;


        internal void Init(CharacterController controller)
        {
            Controller = controller;
            OnEnable();
        }


        private void OnEnable()
        {
            if (Controller) Controller.CharacterMoved += Move;
        }

        private void OnDisable()
        {
            if (Controller) Controller.CharacterMoved -= Move;
        }

        private void Move(Vector2 direction)
        {
            var delta = direction * (speed * Time.deltaTime);

            transform.position += new Vector3(delta.x, 0, delta.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            var iObject = other.gameObject.GetComponent<InteractiveObject>();
            if (iObject)
                Controller.AddInteractableObjects(iObject);
        }

        private void OnTriggerExit(Collider other)
        {
            var iObject = other.gameObject.GetComponent<InteractiveObject>();
            if (iObject)
                Controller.RemoveInteractableObjects(iObject);
        }
    }
}