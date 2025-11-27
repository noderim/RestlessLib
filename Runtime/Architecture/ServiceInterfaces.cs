namespace RestlessLib.Architecture
{
    public interface ITickable
    {
        void Tick(float deltaTime);
    }

    public interface IFixedTickable
    {
        void FixedTick(float fixedDeltaTime);
    }

    public interface IInitializable
    {
        void Initialize();
    }

    // Standard C# interface, good for cleanup
    public interface IDisposable
    {
        void Dispose();
    }
}
