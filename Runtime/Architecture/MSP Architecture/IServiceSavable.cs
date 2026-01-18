namespace RestlessLib.Architecture
{
    /// <summary>
    /// Interface for savable services.
    /// </summary>
    /// <typeparam name="TService">The type of service.</typeparam>
    public interface IServiceSavable<TService, TSavedState>
    {
        TSavedState SaveState(TService service);
        bool LoadState(TService service, TSavedState savedState);
    }
}