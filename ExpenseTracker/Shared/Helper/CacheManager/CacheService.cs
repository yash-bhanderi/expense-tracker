using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using CodeCommandos.Shared.Helper.Utilities;
using Microsoft.Extensions.Caching.Distributed;

namespace CodeCommandos.Shared.Helper.CacheManager;

public interface ICacheService<T>
{
    Task<T> GetItemAsync(string key);
    Task SetItemAsync(string key, T value, double? expireInSeconds = null);
    Task RemoveItemAsync(string key);
}

[ExcludeFromCodeCoverage]
public class CacheService<TCacheValue> : ICacheService<TCacheValue>
{
    private readonly ILogger<CacheService<TCacheValue>> _logger;
    private readonly IDistributedCache _distributedCache;

    public CacheService(ILogger<CacheService<TCacheValue>> log, IDistributedCache distributedCache)
    {
        _logger = log;
        _logger.LogInformation($"Initialize cache manager instance of {typeof(TCacheValue).FullName}");
        _distributedCache = distributedCache;
    }

    public async Task<TCacheValue> GetItemAsync(string key)
    {
        if (key.IsEmpty()) throw new ArgumentNullException(nameof(key));
        try
        {

            var value = await GetItemsAsync(key);
            return string.IsNullOrWhiteSpace(value) ? default : JsonSerializer.Deserialize<TCacheValue>(value);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Redis failed to Get the key with key name: {Key}", key);
        }
        return default;
    }

    private async Task<string> GetItemsAsync(string key)
    {
        return await _distributedCache.GetStringAsync(key);
    }

    public async Task SetItemAsync(string key, TCacheValue value, double? expireInSeconds = null)
    {
        try
        {
            var distributedCacheEntryOptions = GetDistributedCacheEntryOptions(expireInSeconds);
            var valueString = JsonSerializer.Serialize(value);
            if (!string.IsNullOrEmpty(valueString))
            {
                if (distributedCacheEntryOptions is not null)
                    await _distributedCache.SetStringAsync(key, valueString, distributedCacheEntryOptions);
                else
                    await _distributedCache.SetStringAsync(key, valueString);
            }
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Redis failed to upsert the key with key name: {Key}", key);
        }
    }
    public async Task RemoveItemAsync(string key)
    {
        var itemKey = key.IsEmpty() ? string.Empty : key;
        try
        {
            await _distributedCache.RemoveAsync(itemKey);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Redis failed to remove the key {Key}", itemKey);
        }
    }
    private static DistributedCacheEntryOptions GetDistributedCacheEntryOptions(double? expireInSeconds)
    {
        DistributedCacheEntryOptions distributedCacheEntryOptions = null;
        if (expireInSeconds is > 0)
        {
            distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds((double)expireInSeconds),
            };
        }

        return distributedCacheEntryOptions;
    }
}
