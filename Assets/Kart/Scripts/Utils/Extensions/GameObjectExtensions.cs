using UnityEngine;

namespace kart.Kart.Scripts.Utils.Extensions {
    public static class GameObjectExtensions {
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component {
            T component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }
    }
}