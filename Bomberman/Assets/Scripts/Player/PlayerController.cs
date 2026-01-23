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
            if (inputProvider == null || movement == null || stats == null) return;
            Vector2 direction = inputProvider.MoveDirection;
            float speed = stats.MoveSpeed;

            movement.SetInput(direction, speed);

            if(playerAnimation != null)
            {
                playerAnimation.UpdateAnimation(direction);
            }
        }
    }
}
