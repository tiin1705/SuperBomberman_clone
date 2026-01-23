using UnityEngine;
namespace Player.Visuals
{
public class PlayerAnimation : MonoBehaviour
{
        [SerializeField] private Animator animator;
        private Vector2 lastDirection = Vector2.down;

        private void Awake()
        {
            if(animator == null ) animator = GetComponent<Animator>();

        }
        public void UpdateAnimation(Vector2 direction)
        {
            bool isMoving = direction != Vector2.zero;

            animator.SetBool("IsMoving", isMoving);

            if (isMoving)
            {
                lastDirection = direction;
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
            }
            else
            {
                animator.SetFloat("Horizontal", lastDirection.x);
                animator.SetFloat("Vertical", lastDirection.y);
            }
        }

    }
}
