using UnityEngine;

namespace Core.Inputs
{
    /// <summary>
    /// Interface abstraction for getting input. 
    /// Allows controlling the player via Keyboard, AI script, or Network data.
    /// </summary>
    public interface IInputProvider
    {
        Vector2 MoveDirection { get; }
        bool PlaceBomb { get; }
    }
}
