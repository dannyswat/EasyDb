using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlMapper
{
	/// <summary>
	/// Utility class for static cache
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
    public class StaticCache<TKey, TValue> where TValue : class
    {
		Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();

		public TValue GetOrAddMapping(TKey key)
		{
			return GetMapping(key) ?? AddOrUpdateMapping(key);
		}

		public TValue GetMapping(TKey key)
		{
			if (cache.ContainsKey(key))
				return cache[key];
			else
				return default;
		}

		public TValue AddOrUpdateMapping(TKey key)
		{
			if (cache.ContainsKey(key))
				cache[key] = CreateInstance(key);
			else
				cache.Add(key, CreateInstance(key));

			return cache[key];
		}

		public void RemoveMapping(TKey key)
		{
			if (cache.ContainsKey(key))
				cache.Remove(key);
		}

		protected virtual TValue CreateInstance(TKey key)
		{
			return (TValue)Activator.CreateInstance(typeof(TValue), key);
		}
	}
}
