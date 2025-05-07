namespace SIMA.Application.Query.Features.Auths.SharedMemory
{
    public interface ICacheProvider
    {
        void Set<T>(string key, T value, TimeSpan? timeout, int dbId) where T : class;
        bool TryGet<T>(string key, int dbId, out T value) where T : class;
        bool Remove(string key, int dbId);
        bool IsInCache(string key, int dbId);
        long GetCounter(string key, int dbId);
        void Reset(string key, int dbId);

    }
}

