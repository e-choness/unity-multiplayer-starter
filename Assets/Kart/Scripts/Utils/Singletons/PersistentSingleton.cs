using UnityEngine;

namespace kart.Kart.Scripts.Utils.Singletons
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance is null)
            {
                Instance = this as T;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        protected void OnDestroy()
        {
            Instance = null;
        }
    }
}