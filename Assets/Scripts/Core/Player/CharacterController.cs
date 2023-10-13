using System.Collections.Generic;
using System.Linq;
using Interaction;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Character))]
    public class CharacterController : MonoBehaviour
    {
        public delegate void MovementControlDelegate(Vector2 direction);

        public event MovementControlDelegate CharacterMoved;

        [Header("Player Controls")] [SerializeField]
        private KeyCode up = KeyCode.W;

        [SerializeField] private KeyCode down = KeyCode.S;
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode right = KeyCode.D;
        [SerializeField, Space] private KeyCode interaction = KeyCode.E;

        [Header("Character UI")] [SerializeField]
        private GameObject characterHUD;

        private Character _character;
        private bool _isInteracting;

        private bool _isInteractPressed;

        private List<InteractiveObject> _interactableObjects;
        private InteractiveObject _currenInteractiveObject;

        private void Start()
        {
            _character = GetComponent<Character>();
            if (!_character)
            {
                Destroy(gameObject);
                return;
            }

            _character.Init(this);
            _interactableObjects = new List<InteractiveObject>();
            DeactivatePanel();
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (!Input.anyKey) return;
            if (CheckInteractionInput()) return;

            CheckMovementInput();
        }

        private bool CheckInteractionInput()
        {
            if (_interactableObjects.Count <= 0) return false;

            if (!Input.GetKeyDown(interaction)) return _isInteracting;

            if (_isInteracting)
            {
                if (_currenInteractiveObject)
                    _currenInteractiveObject.OnInteractionEnd();

                DeactivatePanel();
                _currenInteractiveObject = null;
            }
            else
            {
                ActivatePanel();
                foreach (var iObject in _interactableObjects.Where(iObject => iObject.OnInteractionStart(this)))
                {
                    _currenInteractiveObject = iObject;
                    break;
                }
            }

            _isInteracting = !_isInteracting;

            return _isInteracting;
        }

        private void CheckMovementInput()
        {
            var movementDirection = (Input.GetKey(up) ? Vector2.up : Vector2.zero)
                                    + (Input.GetKey(down) ? Vector2.down : Vector2.zero)
                                    + (Input.GetKey(left) ? Vector2.left : Vector2.zero)
                                    + (Input.GetKey(right) ? Vector2.right : Vector2.zero);

            if (movementDirection == Vector2.zero) return;

            CharacterMoved?.Invoke(movementDirection);
        }

        private void ActivatePanel()
        {
            characterHUD.SetActive(true);
        }

        private void DeactivatePanel()
        {
            characterHUD.SetActive(false);
        }

        public void AddInteractableObjects(InteractiveObject iObject)
        {
            _interactableObjects.Insert(0, iObject);
        }

        public void RemoveInteractableObjects(InteractiveObject iObject)
        {
            _interactableObjects.Remove(iObject);
        }
    }
}