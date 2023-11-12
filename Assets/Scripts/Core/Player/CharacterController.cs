using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Game_Systems;
using Interaction;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Character))]
    public class CharacterController : MonoBehaviour
    {
        public delegate void MovementControlDelegate(Vector2 direction);

        public event MovementControlDelegate CharacterMoved;

        public int PlayerID => playerID;

        [SerializeField] private int playerID;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private GameObject characterHUD;

        private Character _character;

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
            SetPlayerInput();
        }

        public void SetPlayerInput(PlayerInput playerInput = null)
        {
            this.playerInput = playerInput;
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (!playerInput) return;
            if (!Input.anyKey) return;
            if (CheckInteractionInput()) return;

            CheckMovementInput();
        }

        private bool CheckInteractionInput()
        {
            if (_interactableObjects.Count <= 0) return false;

            if (!playerInput.IsInteractionButtonPressed()) return false;

            foreach (var iObject in _interactableObjects.Where(iObject => iObject.OnInteractionStart(this)))
            {
                _currenInteractiveObject = iObject;
                _currenInteractiveObject.OnInteractionCompleted += OnInteractionCompleted;
                ActivatePanel();
                return true;
            }

            return false;
        }

        private void OnInteractionCompleted(bool interactionComplete)
        {
            _currenInteractiveObject.OnInteractionCompleted -= OnInteractionCompleted;

            DeactivatePanel();
            _currenInteractiveObject = null;
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