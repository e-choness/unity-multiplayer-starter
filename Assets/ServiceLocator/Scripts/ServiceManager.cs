using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Scripts
{
    public class ServiceManager
    {
        private readonly Dictionary<Type, object> _services = new();
        public IEnumerable<object> RegisterServices => _services.Values;
        
        public bool TryGet<T>(out T service) where T : class
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out object serviceOjb))
            {
                service = serviceOjb as T;
                return true;
            }

            service = null;
            return false;
        }

        public ServiceManager Register<T>(T service)
        {
            var type = typeof(T);
            if (_services.TryAdd(type, service))
            {
                Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered.");
            }

            return this;
        }

        public ServiceManager Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
            {
                throw new ArgumentException("Type of service does not match type of service interface",
                    nameof(service));
            }

            if (!_services.TryAdd(type, service))
            {
                Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered");
            }

            return this;
        }
    }
}