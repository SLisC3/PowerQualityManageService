using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;

public class CacheHelper
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
            .SetSlidingExpiration(TimeSpan.FromMinutes(20));

    public CacheHelper(IMemoryCache cache)
    {
        _cache = cache;
    }

    public bool TryGetValue<T>(string key, out T value)
    {
        if (_cache.TryGetValue(key, out value))
        {
            return true;
        }
        return false;
    }

    public void Set<T>(string key, T value)
    {
        _cache.Set(key, value, _options);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}

