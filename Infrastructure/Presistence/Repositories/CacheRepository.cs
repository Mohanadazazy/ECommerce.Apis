﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer multiplexer) : ICacheRepository
    {
        private readonly IDatabase _database = multiplexer.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
             return !value.IsNullOrEmpty ? value : default;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var RedisValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, RedisValue, duration);
        }
    }
}
