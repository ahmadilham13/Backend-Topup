namespace backend.BaseModule.Interfaces.Shared;

public interface ICacheService
{
    Task<T> GetRedisCache<T>(string cacheKey);
    Task WriteRedisCache(string cacheKey, string cacheValue);
    Task UpdateRedisCache(string cacheKey, string cacheValue);
    Task DeleteCache(string cacheKey);

}