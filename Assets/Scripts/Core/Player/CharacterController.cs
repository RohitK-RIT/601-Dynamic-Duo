using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interaction;
using Systems.Player_Input;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Character))]
    public class CharacterController : MonoBehaviour
    {
        public delegate void MovementControlDelegate(Vector2 direction);

        public event MovementControlDelegate CharacterMoved;

        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private GameObject characterHUD;

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

        public PlayerInput GetPlayerInput()
        {
            StartCoroutine(SetPlayerInputNull());
            return playerInput;
        }

        private IEnumerator SetPlayerInputNull()
        {
            yield return new WaitForSeconds(0.1f);
            playerInput = null;
        }

        public void SetPlayerInput(PlayerInput playerInput = null)
        {
            this.playerInput = playerInput;
        }

        private void Update()
        {
            if (playerInput)
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

            if (_isInteracting && playerInput.IsInteractionCancelledButtonPressed())
            {
                OnInteractionCompleted();
            }
            else if (!_isInteracting && playerInput.IsInteractionButtonPressed())
            {
                ActivatePanel();
                foreach (var iObject in _interactableObjects.Where(iObject => iObject.OnInteractionStart(this)))
                {
                    _currenInteractiveObject = iObject;
                    _currenInteractiveObject.OnInteractionCompleted += OnInteractionCompleted;
                    _isInteracting = true;
                    break;
                }
            }

            return _isInteracting;
        }

        private void OnInteractionCompleted()
        {
            if (_currenInteractiveObject)
                _currenInteractiveObject.OnInteractionEnd();

            _currenInteractiveObject.OnInteractionCompleted -= OnInteractionCompleted;

            DeactivatePanel();
            _currenInteractiveObject = null;
            _isInteracting = false;
        }

        private void CheckMovementInput()
        {
            var movementDirection = playerInput.GetMovementDirection();

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