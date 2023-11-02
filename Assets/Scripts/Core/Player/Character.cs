using Interaction;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [SerializeField] public float speed = 10f;
        
        private CharacterController _controller;
        private Rigidbody _characterRigidbody;

        internal void Init(CharacterController controller)
        {
            _controller = controller;
            _characterRigidbody = GetComponent<Rigidbody>();
            OnEnable();
        }


        private void OnEnable()
        {
            if (_controller) _controller.CharacterMoved += Move;
        }

        private void OnDisable()
        {
            if (_controller) _controller.CharacterMoved -= Move;
        }

        private void Move(Vector2 direction)
        {
            Debug.Log("Aa rha hai kya dekh");
            var delta = direction * (speed * Time.deltaTime);
            _characterRigidbody.velocity = new Vector3(delta.x, 0, delta.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            var iObject = other.gameObject.GetComponent<InteractiveObject>();
            if (iObject)
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