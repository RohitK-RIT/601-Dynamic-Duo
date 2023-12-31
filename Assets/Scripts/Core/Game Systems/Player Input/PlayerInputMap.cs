using UnityEngine;

namespace Core.Game_Systems.Player_Input
{
    [CreateAssetMenu(fileName = "Player Input Map", menuName = "Player Input Map Asset", order = 0)]
    public class PlayerInputMap : ScriptableObject
    {
        [Header("Navigation")]
        [SerializeField] private KeyCode up = KeyCode.W;
        [SerializeField] private KeyCode down = KeyCode.S;
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode right = KeyCode.D;

        [Header("Interaction")]
        [SerializeField, Space] private KeyCode interaction = KeyCode.E;
        [SerializeField] private KeyCode interactionCancelled = KeyCode.Q;

        public KeyCode Up => up;
        public KeyCode Down => down;
        public KeyCode Left => left;
        public KeyCode Right => right;
        public KeyCode Interaction => interaction;
        public KeyCode InteractionCancelled => interactionCancelled;
        

        private bool _interactionButtonPress;

        private void OnEnable()
        {
            _interactionButtonPress = false;
        }

        public bool IsInteractionButtonPressed()
        {
            if (_interactionButtonPress)
            {
                if (Input.GetKeyUp(interaction))
                    _interactionButtonPress = false;
                return false;
            }
            
            if (Input.GetKeyDown(interaction))
            {
                _interactionButtonPress = true;
                return true;
            }

            return false;
        }
        public bool IsInteractionCancelledButtonPressed()
        {
            return Input.GetKeyDown(interactionCancelled);
        }

        public Vector2 GetMovementDirection()
        {
            return (Input.GetKey(up) ? Vector2.up : Vector2.zero)
                                    + (Input.GetKey(down) ? Vector2.down : Vector2.zero)
                                    + (Input.GetKey(left) ? Vector2.left : Vector2.zero)
                                    + (Input.GetKey(right) ? Vector2.right : Vector2.zero);
        }
    }
}