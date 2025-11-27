using System;
using System.Collections.Generic;
using UnityEngine;

namespace RestlessLib.Architecture
{
    /// <summary>
    /// A simple static Service Locator for registering and retrieving systems.
    /// </summary>
    public static class SystemLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a service instance of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the service interface or class.</typeparam>
        /// <param name="service">The instance to register.</param>
        public static void Register<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"SystemLocator: Service of type {type.Name} is already registered. Overwriting.");
                _services[type] = service;
            }
            else
            {
                _services.Add(type, service);
            }
        }

        /// <summary>
        /// Unregisters a service of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the service to remove.</typeparam>
        public static void Unregister<T>()
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }
        /// <summary>
        /// Unregisters a service of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the service to remove.</typeparam>
        public static void Unregister<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type) && _services[type].Equals(service))
            {
                _services.Remove(type);
            }
        }

        /// <summary>
        /// Retrieves a registered service instance.
        /// </summary>
        /// <typeparam name="T">The type of the service to retrieve.</typeparam>
        /// <returns>The service instance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the service is not registered.</exception>
        public static T Get<T>()
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            throw new KeyNotFoundException($"SystemLocator: Service of type {type.Name} not found.");
        }

        /// <summary>
        /// Tries to retrieve a registered service instance safely.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="service">The output service instance.</param>
        /// <returns>True if found, false otherwise.</returns>
        public static bool TryGet<T>(out T service)
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var instance))
            {
                service = (T)instance;
                return true;
            }

            service = default;
            return false;
        }

        /// <summary>
        /// Clears all registered services.
        /// </summary>
        public static void Clear()
        {
            _services.Clear();
        }
    }
}
