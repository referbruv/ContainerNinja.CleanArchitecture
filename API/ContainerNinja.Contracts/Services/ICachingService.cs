namespace ContainerNinja.Contracts.Services
{
    public interface ICachingService
    {
        T? GetItem<T>(string cacheKey);
        T SetItem<T>(string cacheKey, T item);
    }
}
