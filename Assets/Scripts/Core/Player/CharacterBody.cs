using Interaction;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class CharacterBody : MonoBehaviour
    {
        [SerializeField] public float speed = 10f;

        private CharacterController _controller;
        private Rigidbody _characterRigidbody;

        internal void Init(CharacterController controller)
        {
            _controller = controller;
            _characterRigidbody = GetComponent<Rigidbody>();
        }
        
        public void Move(Vector2 direction)
        {
            var delta = direction * (speed * Time.deltaTime) * new Vector2(1, 1.5f) /*Movement factor added by Matt*/;
            _characterRigidbody.velocity = new Vector3(delta.x, 0, delta.y);
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