using UnityEngine;
using Core.Inputs;
using Player.Data;
using Player.Logic;
using System;
using Player.Visuals;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerAnimation playerAnimation;
        private IInputProvider inputProvider;

        [Header("Data")]
        [SerializeField] private PlayerStats stats;

        private void Awake()
        {
            inputProvider = GetComponent<IInputProvider>();
            if (movement == null)
            {
                movement = GetComponent<PlayerMovement>();
            }

            if(playerAnimation == null)
            {
                playerAnimation = GetComponent<PlayerAnimation>();
            }

        }

        private void Update()
        {
            CheckComponents();
            GetDirection();
            GetSpeed();
            SetDirection();
            SetAnimation();
        }

        private void CheckComponents(){
            if (inputProvider == null || movement == null || stats == null) return;
        }
        private void GetDirection(){
            Vector2 direction = inputProvider.MoveDirection;
        }
        private void GetSpeed(){
            float speed = stats.MoveSpeed;
        }

        private void SetDirection(){
            movement.SetInput(direction, speed);
        }

        private void SetAnimation(){
            if(playerAnimation != null)
            {
                playerAnimation.UpdateAnimation(direction);
            }
        }
    }
}
