using Interaction;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class CharacterBody : MonoBehaviour
    {
        [SerializeField] public float speed = 10f;

        public Rigidbody CharacterRigidBody { get; private set; }

        private CharacterController _controller;

        internal void Init(CharacterController controller)
        {
            _controller = controller;
            CharacterRigidBody = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 direction)
        {
            var newVelocity = speed * direction.normalized;
            CharacterRigidBody.velocity = new Vector3(newVelocity.x, 0, newVelocity.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            var iObject = other.gameObject.GetComponent<InteractiveObject>();
            if (iObject && iObject.Interactable)
                _controller.AddInteractableObjects(iObject);
        }

        private void OnTriggerExit(Collider other)
        {
            var iObject = other.gameObject.GetComponent<InteractiveObject>();
            if (iObject)
                _controller.RemoveInteractableObjects(iObject);
        }
    }
}