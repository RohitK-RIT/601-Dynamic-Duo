using System.Collections.Generic;
using System.Linq;
using Core.Game_Systems;
using Core.Game_Systems.Player_Input;
using Interaction;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterBody))]
    public class CharacterController : MonoBehaviour
    {
        public PlayerID PlayerID => playerID;

        [SerializeField] private PlayerID playerID;
        [FormerlySerializedAs("inputActionMap")] [SerializeField] private ActionMap actionMap;
        [SerializeField] private GameObject characterHUD;

        private CharacterInputListener _inputListener;
        private CharacterBody _characterBody;
        private List<InteractiveObject> _interactableObjects;

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
            _inputListener ??= new CharacterInputListener(PlayerID, actionMap, this);
            _inputListener.TryEnable();

            _inputListener.OnPlayerMoved += PlayerMove;
            _inputListener.OnPlayerInteract += PlayerInteract;
        }

        private void OnDisable()
        {
            DisableInput();
        }

        private void DisableInput()
        {
            _inputListener.Disable();

            _inputListener.OnPlayerMoved -= PlayerMove;
            _inputListener.OnPlayerInteract -= PlayerInteract;
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
            if(!iObject.Interactable) return;
            
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