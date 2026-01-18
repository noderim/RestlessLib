using Sirenix.OdinInspector;
using UnityEngine;

namespace RestlessLib.Architecture
{
    [System.Serializable]
    public class LifecycleBinder
    {
        [Header("State")]
        [ShowInInspector, ReadOnly]
        protected object Receiver;
        bool _awakePassed;
        bool _startPassed;
        bool _enabled;

        [Header("Cached Interfaces")]
        [ShowInInspector, ReadOnly]
        IAwakeable _awakeable;
        [ShowInInspector, ReadOnly]
        IStartable _startable;
        [ShowInInspector, ReadOnly]
        IUpdatable _updatable;
        [ShowInInspector, ReadOnly]
        IFixedUpdatable _fixedUpdatable;
        [ShowInInspector, ReadOnly]
        ILateUpdatable _lateUpdatable;
        [ShowInInspector, ReadOnly]
        IDestroyable _destroyable;
        [ShowInInspector, ReadOnly]
        IEnableDisable _enableDisable;


        public void Bind(object receiver)
        {
            if (receiver == null)
            {
                Debug.LogError("Bind called with null receiver");
                return;
            }

            Receiver = receiver;

            CacheInterfaces();

            // Late binding for Awake/Start if Setup happens AFTER them
            if (_awakePassed)
                _awakeable?.OnAwake();

            if (_enabled)
                _enableDisable?.OnEnable();

            if (_startPassed)
                _startable?.OnStart();
        }

        void CacheInterfaces()
        {
            _awakeable = Receiver as IAwakeable;
            _startable = Receiver as IStartable;
            _updatable = Receiver as IUpdatable;
            _fixedUpdatable = Receiver as IFixedUpdatable;
            _lateUpdatable = Receiver as ILateUpdatable;
            _destroyable = Receiver as IDestroyable;
            _enableDisable = Receiver as IEnableDisable;
        }

        public void Awake()
        {
            _awakePassed = true;
            _awakeable?.OnAwake();
        }

        public void Start()
        {
            _startPassed = true;
            _startable?.OnStart();
        }

        public void Update()
        {
            _updatable?.OnUpdate(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            _fixedUpdatable?.OnFixedUpdate(Time.fixedDeltaTime);
        }

        public void LateUpdate()
        {
            _lateUpdatable?.OnLateUpdate(Time.deltaTime);
        }

        public void OnEnable()
        {
            _enableDisable?.OnEnable();
            _enabled = true;
        }

        public void OnDisable()
        {
            _enableDisable?.OnDisable();
            _enabled = false;
        }

        public void OnDestroy()
        {
            _destroyable?.OnDestroy();
        }
    }
}