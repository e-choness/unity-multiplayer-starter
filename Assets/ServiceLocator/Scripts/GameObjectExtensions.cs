using UnityEngine;

namespace ServiceLocator.Scripts
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject.TryGetComponent<T>(out var component))
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
        
        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;

        public static void DestroyChildren(this GameObject gameObject)
        {
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(gameObject.transform.GetChild(i));
            }
        }

        public static void EnableChildren(this GameObject gameObject)
        {
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        public static void DisableChildren(this GameObject gameObject)
        {
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}