using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game_Systems;
using Interaction;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterBody))]
    public class CharacterController : MonoBehaviour
    {
        public PlayerID PlayerID => playerID;

        [SerializeField] private PlayerID playerID;
        [SerializeField] private GameObject characterHUD;

        private CharacterInputHandler _inputHandler;
        private CharacterBody _characterBody;
        private List<InteractiveObject> _interactableObjects;

        private Animator _animator;

        private bool _firstRun = true;
        private static readonly int XDir = Animator.StringToHash("xDir");

        private void Start()
        {
            _characterBody = GetComponent<CharacterBody>();
            if (!_characterBody)
            {
                Destroy(gameObject);
                return;
            }

            _characterBody.Init(this);
            _animator = GetComponent<Animator>();
            _interactableObjects = new List<InteractiveObject>();
            DeactivatePanel();
        }

        private void OnEnable()
        {
            if (_firstRun)
            {
                _firstRun = false;
                Invoke(nameof(EnableInput), 0.2f);
            }
            else
            {
                EnableInput();
            }
        }

        private void EnableInput()
        {
            _inputHandler ??= new CharacterInputHandler(PlayerID, this);
            _inputHandler.Enable();

            _inputHandler.OnPlayerMoved += PlayerMove;
            _inputHandler.OnPlayerInteract += PlayerInteract;
        }

        private void OnDisable()
        {
            DisableInput();
        }

        private void DisableInput()
        {
            _inputHandler.Disable();

            _inputHandler.OnPlayerMoved -= PlayerMove;
            _inputHandler.OnPlayerInteract -= PlayerInteract;
        }

        private void PlayerMove(Vector2 movementDirection)
        {
            _characterBody.Move(movementDirection);
            if (_animator)
                _animator.SetFloat(XDir, movementDirection.x);
        }

        private void PlayerInteract()
        {
            if (_interactableObjects.Count <= 0) return;
            if (!_interactableObjects.Any(iObject => iObject.OnHandleInteractee(this))) return;

            DisableInput();
            ActivatePanel();
        }

        public void OnInteractionEnd(bool interactionComplete)
        {
            EnableInput();
            DeactivatePanel();
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

        private void FixedUpdate()
        {
            if (_characterBody.CharacterRigidBody.velocity.x == 0 && _animator)
            {
                _animator.SetFloat(XDir, 0);
            }
        }
    }
}