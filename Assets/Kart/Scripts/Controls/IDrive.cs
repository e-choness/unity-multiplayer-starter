using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    public interface IDrive
    {
        Vector2 Move { get; }
        bool IsBreaking { get; }
        void Enable();
    }
}