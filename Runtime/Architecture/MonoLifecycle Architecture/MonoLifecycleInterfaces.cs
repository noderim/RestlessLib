namespace RestlessLib.Architecture
{
    public interface IUpdatable
    {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdatable
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
    public interface ILateUpdatable
    {
        void OnLateUpdate(float deltaTime);
    }

    public interface IInitializable
    {
        void Initialize();
    }
    public interface IStartable
    {
        public void OnStart();
    }
    public interface IAwakeable
    {
        public void OnAwake();
    }
    public interface IDestroyable
    {
        public void OnDestroy();
    }
    public interface IEnableDisable
    {
        public void OnEnable();
        public void OnDisable();
    }
}
