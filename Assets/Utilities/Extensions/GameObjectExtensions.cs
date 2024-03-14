using UnityEngine;

namespace kart.Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject obj) where T : Component
        {
            if (!obj.TryGetComponent<T>(out var component))
            {
                component = obj.AddComponent<T>();
            }

            return component;
        }
    }
}
