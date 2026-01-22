
using UnityEngine;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Bomberman/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [Header("Movement")]
        public float MoveSpeed = 5f;

        [Header("Combat")]
        public int MaxBombs = 1;
        public int ExplosionRange = 2;
    }
}