namespace U.Hangfire
{
    public interface IHangfireDataProvider
    {
        bool Exists(string jobId);
    }
}
