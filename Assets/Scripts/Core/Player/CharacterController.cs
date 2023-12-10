using System.Collections.Generic;
using System.Linq;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using Interaction;
using Mini_Games;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterBody))]
    public class CharacterController : MonoBehaviour
    {
        public PlayerID PlayerID => playerID;

        [SerializeField] private PlayerID playerID;

        [FormerlySerializedAs("inputActionMap")] [SerializeField]
        private ActionMap actionMap;

        [SerializeField] private GameObject characterHUD;

        private CharacterInputListener _inputListener;
        public CharacterHUDInputListener _hudListener;
        private CharacterBody _characterBody;
        private List<InteractiveObject> _interactableObjects;
        private InteractiveObject _currentIObject;

        private Animator _animator;

        private bool _firstRun = true;
        private static readonly int XDir = Animator.StringToHash("xDir");

        Canvas popupCanvas;
        RectTransform popupCanvasRectTrans;

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


            //Set pop up canvas
            Transform childObject = transform.Find("InteractionPopUp");
            popupCanvas = childObject.GetComponent<Canvas>();
            popupCanvas.enabled = false;
            popupCanvasRectTrans = popupCanvas.GetComponent<RectTransform>();

            TogglePanel(false);
        }


        public string GetCancelKey()
        {
            _hudListener ??= new CharacterHUDInputListener(PlayerID, ActionMap.HUD, this);
            return _hudListener.CancelKey;
        }

        private void OnEnable()
        {
            if (_firstRun)
            {
                _firstRun = false;
                Invoke(nameof(InitInputListeners), 0.2f);
                Invoke(nameof(EnableInput), 0.2f);
            }
            else
            {
                EnableInput();
            }
        }

        private void OnDisable()
        {
            DisableInput();
        }

        private void InitInputListeners()
        {
            _inputListener ??= new CharacterInputListener(PlayerID, actionMap, this);
            _hudListener ??= new CharacterHUDInputListener(PlayerID, ActionMap.HUD, this);
        }

        private void EnableInput()
        {
            _inputListener.TryEnable();

            _inputListener.OnPlayerMoved += PlayerMove;
            _inputListener.OnPlayerInteract += PlayerInteract;
        }

        private void DisableInput()
        {
            _inputListener.Disable();

            _inputListener.OnPlayerMoved -= PlayerMove;
            _inputListener.OnPlayerInteract -= PlayerInteract;
        }

        private void EnableHUDInput()
        {
            _hudListener.TryEnable();

            _hudListener.OnBackPressed += BackPressed;
        }

        private void DisableHUDInput()
        {
            _hudListener.Disable();

            _hudListener.OnBackPressed -= BackPressed;
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
            foreach (var iObject in _interactableObjects.Where(iObject => iObject.OnHandleInteractee(this)))
            {
                _currentIObject = iObject;
                break;
            }

            if (!_currentIObject) return;

            DisableInput();
            if (_currentIObject is not IOMiniGameTrigger || !MiniGame.IsOpen)
                EnableHUDInput();
            TogglePanel(true);
        }

        private void BackPressed()
        {
            if (!_currentIObject) return;

            _currentIObject.OnCancelInteraction();
        }

        public void OnInteractionEnd(bool interactionComplete)
        {
            _interactableObjects = _interactableObjects.FindAll(iObject => iObject.Interactable);
            popupCanvas.enabled = _interactableObjects.Count > 0 || !interactionComplete;
            DisableHUDInput();
            EnableInput();
            TogglePanel(false);
        }

        private void TogglePanel(bool toggle)
        {
            characterHUD.SetActive(toggle);
        }

        public void AddInteractableObjects(InteractiveObject iObject)
        {
            if (!iObject.Interactable) return;

            _interactableObjects.Insert(0, iObject);
            //Show Popup
            popupCanvas.enabled = _interactableObjects.Count > 0;
        }

        public void RemoveInteractableObjects(InteractiveObject iObject)
        {
            _interactableObjects.Remove(iObject);
            //Hide popup
            popupCanvas.enabled = _interactableObjects.Count > 0;
        }

        private void FixedUpdate()
        {
            if (_characterBody.CharacterRigidBody.velocity.x == 0 && _animator)
                _animator.SetFloat(XDir, 0);
        }
    }
}