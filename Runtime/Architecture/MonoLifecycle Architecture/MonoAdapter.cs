using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RestlessLib.Architecture
{
    /// <summary>
    /// Adapter MonoBehaviour that binds lifecycle events to a receiver object. Can be attached to GameObjects as a seperate component or used as a base class.
    /// </summary>
    public class MonoAdapter : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        LifecycleBinder _lifecycleBinder = new LifecycleBinder();

        public void Bind(object receiver)
        {
            _lifecycleBinder?.Bind(receiver);
        }
    }
}
