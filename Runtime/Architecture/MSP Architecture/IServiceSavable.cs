namespace RestlessLib.Architecture
{
    public interface IServiceSavable<TService, TSavedState>
    {
        TSavedState SaveState(TService service);
        bool LoadState(TService service, TSavedState savedState);
    }
}