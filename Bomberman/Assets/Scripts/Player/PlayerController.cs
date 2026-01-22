using UnityEngine;
using Core.Inputs;
using Player.Data;
using Player.Logic;
using System;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private PlayerMovement movement;
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
        }

            private void Update()
        {
            Vector2 director = inputProvider.MoveDirection;
            float speed = stats.MoveSpeed;

            movement.SetInput(director, speed);
        }
    }
}
