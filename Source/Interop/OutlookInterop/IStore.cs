namespace CodeCharm.OutlookInterop
{
    public interface IStore
    {
        IFolder RootMessageFolder { get; }
        string DisplayName { get; }
    }
}