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

        // Biến lưu trạng thái để xử lý logic "Nút bấm sau được ưu tiên"
        private float lastHorizontal;
        private float lastVertical;
        private bool prioritizeHorizontal;

        private void Update()
        {
            // Read standard WASD / Arrow keys
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // LOGIC ƯU TIÊN PHÍM BẤM SAU (Latest Key Priority)
            // 1. Kiểm tra xem phím nào vừa mới được bấm xuống trong frame này
            bool hJustPressed = (horizontal != 0 && lastHorizontal == 0);
            bool vJustPressed = (vertical != 0 && lastVertical == 0);

            // 2. Cập nhật quyền ưu tiên
            if (hJustPressed)
            {
                prioritizeHorizontal = true; // Mới bấm Ngang -> Ưu tiên Ngang
            }
            else if (vJustPressed)
            {
                prioritizeHorizontal = false; // Mới bấm Dọc -> Ưu tiên Dọc
            }
            // Nếu nhả phím Ngang ra, thì tự động chuyển quyền cho Dọc (nếu Dọc đang giữ)
            else if (horizontal == 0)
            {
                prioritizeHorizontal = false;
            }
            // Ngược lại
            else if (vertical == 0)
            {
                prioritizeHorizontal = true;
            }

            // 3. Quyết định hướng đi cuối cùng dựa trên quyền ưu tiên
            if (horizontal != 0 && vertical != 0)
            {
                if (prioritizeHorizontal)
                    vertical = 0; // Ưu tiên ngang thì xóa dọc
                else
                    horizontal = 0; // Ưu tiên dọc thì xóa ngang
            }

            MoveDirection = new Vector2(horizontal, vertical);

            // Lưu lại trạng thái input để frame sau so sánh
            lastHorizontal = horizontal;
            lastVertical = vertical;
            
            // Read Bomb Input (Space or Button 0)
            // GetButtonDown returns true only on the frame the button is pressed
            PlaceBomb = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);
        }
    }
}
