using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocator.Scripts
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _global;
        private static Dictionary<Scene, ServiceLocator> _sceneContainers;

        private readonly ServiceManager _services = new();

        private const string GlobalServiceLocatorName = "ServiceLocatorGlobal";
        private const string SceneServiceLocatorName = "ServiceLocatorScene";

        public static ServiceLocator Global
        {
            get
            {
                if (_global != null) return _global;

                if (FindFirstObjectByType<ServiceLocatorGlobalBootstrapper>() is { } found)
                {
                    found.BootstrapOnDemand();
                    return _global;
                }
                
                // bootstrap or initialize the new instance of global when non available
                var container = new GameObject(GlobalServiceLocatorName, typeof(ServiceLocator));
                container.AddComponent<ServiceLocatorGlobalBootstrapper>().BootstrapOnDemand();
                return _global;
            }
        }

        private static List<GameObject> _tempSceneGameObject;
        
        public static ServiceLocator ForSceneOf(MonoBehaviour monoBehaviour)
        {
            var scene = monoBehaviour.gameObject.scene;

            // If the container is registered in the scene container
            if (_sceneContainers.TryGetValue(scene, out var container) && container != monoBehaviour)
            {
                return container;
            }
            
            // If not look through the root object of the scene and try to find one there.
            _tempSceneGameObject.Clear();
            scene.GetRootGameObjects(_tempSceneGameObject);

            foreach (GameObject obj in _tempSceneGameObject.Where(obj =>
                         obj.GetComponent<ServiceLocatorSceneBootstrapper>() != null))
            {
                if (obj.TryGetComponent(out ServiceLocatorSceneBootstrapper bootstrapper) &&
                    bootstrapper.Container != monoBehaviour)
                {
                    bootstrapper.BootstrapOnDemand();
                    return bootstrapper.Container;
                }
            }
            
            // If both cases above do not have any Scene Bootstrapper, simply return global one.
            return Global;
        }
    }
}