using UnityEngine;
using Core.Inputs;

namespace Player
{
    /// <summary>
    /// Reads direct input from Unity's Input System (Legacy or New).
    /// Used for the LOCAL player only.
    /// </summary>
    public class PlayerInput : MonoBehaviour, IInputProvider
    {
        public Vector2 MoveDirection { get; private set; }
        public bool PlaceBomb { get; private set; }

        private void Update()
        {
            // Read standard WASD / Arrow keys
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            MoveDirection = new Vector2(horizontal, vertical);
            
            // Normalize to prevent faster diagonal movement
            if (MoveDirection.sqrMagnitude > 1)
            {
                MoveDirection.Normalize();
            }

            // Read Bomb Input (Space or Button 0)
            // GetButtonDown returns true only on the frame the button is pressed
            PlaceBomb = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);
        }
    }
}
