
using System;
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

        private void Awake(){
            if(rb == null) rb = GetComponent<Rigidbody2D>();

            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        public void SetInput(Vector2 direction, float speed)
        {
            moveDirection = direction;
            currentSpeed = speed;
        }

        private void Update()
        {
           if(!isMoving && moveDirection != Vector2.zero){
                StartCoroutine(MoveRoutine(moveDirection));
            }
        }

        private IEnumerator MoveRoutine(Vector2 direction){
            isMoving = true;

            Vector2 startPos = new Vector2(Mathf.Round(rb.position.x), (Mathf.Round(rb.position.y)));
            Vector2 targetPos = startPos + direction;

            if(Physics2D.OverlapCircle(targetPos, 0.1f, obstacleLayer)){
                isMoving = false;
                yield break;
            }

            float duration = 1f / currentSpeed;
            float elapsed = 0;
            while(elapsed < duration){
                elapsed += Time.deltaTime;
                float percent = elapsed / duration;

                rb.MovePosition(Vector2.Lerp(startPos, targetPos, percent));
                yield return null;
            }
            rb.MovePosition(targetPos);
            rb.position = targetPos;
            isMoving = false;
        }
        
    }
}
