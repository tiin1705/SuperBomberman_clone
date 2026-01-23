using System.Collections;
using UnityEngine;

namespace Player.Logic
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;

        [Header("Setting")]
        [SerializeField] private LayerMask obstacleLayer;
        
        private float currentSpeed;
        private Vector2 moveDirection;
        private bool isMoving = false; 

        private void Awake()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        public void SetInput(Vector2 direction, float speed)
        {
            moveDirection = direction;
            currentSpeed = speed;
        }

        private void Update()
        {
            if (!isMoving && moveDirection != Vector2.zero)
            {
                StartCoroutine(MoveRoutine());
            }
        }

        private IEnumerator MoveRoutine()
        {
            isMoving = true;

            // Vòng lặp liên tục
            while (moveDirection != Vector2.zero)
            {
                Vector2 startPos = new Vector2(Mathf.Round(rb.position.x), Mathf.Round(rb.position.y));
                Vector2 direction = moveDirection; // Lưu hướng hiện tại của bước đi này
                Vector2 targetPos = startPos + direction;

                // --- 1. Check Va Chạm (Sửa lại) ---
                Vector2 rayOrigin = startPos + new Vector2(0, 0.5f);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, 0.8f, obstacleLayer);

                bool isBlocked = false;
                if (hit.collider != null && hit.collider.gameObject != gameObject)
                {
                    isBlocked = true;
                }

                if (isBlocked)
                {
                    yield return null;
                    continue;// Bị chặn thì quay lại đầu vòng lặp tìm cơ hội khác
                }
                // ----------------------------------

                // --- 2. Di chuyển & Cho phép Đảo Chiều ---
                float duration = 1f / currentSpeed;
                float elapsed = 0;

                while (elapsed < duration)
                {
                    // >>> LOGIC ĐẢO CHIỀU (REVERSE) <<<
                    // Nếu người chơi bấm Ngược lại hướng đang đi (Ví dụ đang đi Trái bấm Phải)
                    if (moveDirection == -direction && moveDirection != Vector2.zero)
                    {
                        // Đổi chiều ngay lập tức: 
                        // Đích đến cũ trở thành Điểm xuất phát mới
                        // Điểm xuất phát cũ trở thành Đích đến mới
                        Vector2 temp = startPos;
                        startPos = targetPos;
                        targetPos = temp;

                        // Đảo ngược thời gian để lerp mượt mà từ vị trí hiện tại
                        elapsed = duration - elapsed;
                        
                        // Cập nhật lại hướng nội bộ
                        direction = moveDirection;
                    }

                    elapsed += Time.deltaTime;
                    float percent = elapsed / duration;
                    rb.MovePosition(Vector2.Lerp(startPos, targetPos, percent));
                    
                    yield return null;
                }

                // Snap vào đích
                rb.MovePosition(targetPos);
                // Loop sẽ tự lặp lại...
            }

            isMoving = false;
        }
    }
}