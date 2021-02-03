using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Pentadome.DotNet.Utilities
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Adds a <paramref name="value"/> with the associated <paramref name="key"/> if <paramref name="key"/> is not yet present in the dictionary and returns <see langword="true"/>.
        /// Otherwise returns <see langword="false"/>
        /// </summary>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
            where TKey : notnull
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            if (@this.ContainsKey(key))
                return false;

            @this.Add(key, value);

            return true;
        }

        /// <summary>
        /// Add a <paramref name="keyValuePair"/> if the key of <paramref name="keyValuePair"/> is not yet present in the dictionary and return <see langword="true"/>.
        /// Otherwise return <see langword="false"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, KeyValuePair<TKey, TValue> keyValuePair)
            where TKey : notnull => @this.TryAdd(keyValuePair.Key, keyValuePair.Value);

        /// <summary>
        /// Add a <paramref name="value"/> with the associated <paramref name="key"/> if <paramref name="key"/> is not yet present in the dictionary and return <see langword="true"/>.
        /// Otherwise replace this value and return <see langword="false"/>.
        /// </summary>
        public static bool AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
             where TKey : notnull
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            if (@this.ContainsKey(key))
            {
                @this.Remove(key);
                @this.Add(key, value);
                return false;
            }
            @this.Add(key, value);
            return true;
        }

        /// <summary>
        /// Add a <paramref name="keyValuePair"/> if the key of <paramref name="keyValuePair"/> is not yet present in the dictionary and return <see langword="true"/>.
        /// Otherwise replace this value and return <see langword="false"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> @this, KeyValuePair<TKey, TValue> keyValuePair)
            where TKey : notnull => @this.AddOrReplace(keyValuePair.Key, keyValuePair.Value);

        /// <summary>
        /// Get the <typeparamref name="TValue"/> of the given <paramref name="key"/> if it is defined in the dictionary.
        /// Otherwise execute <paramref name="factory"/>, add the result to the dictionary with the given <paramref name="key"/> and return the added value.
        /// </summary>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> factory)
            where TKey : notnull
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            if (@this.TryGetValue(key, out var result))
                return result;

            var valueToAdd = factory();
            @this.Add(key, valueToAdd);
            return valueToAdd;
        }
#if SUPPORTSNULLATTRIBUTES
        /// <summary>
        /// Get the <typeparamref name="TValue"/> of the given <paramref name="key"/> if it is defined in the dictionary.
        /// Otherwise execute <paramref name="asyncFactory"/> asynchronously, add the result to the dictionary with the given <paramref name="key"/> and return the added value.
        /// </summary>
        public static async ValueTask<TValue> GetOrCreateAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<Task<TValue>> asyncFactory, bool configureAwait = false)
            where TKey : notnull
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (asyncFactory is null)
                throw new ArgumentNullException(nameof(asyncFactory));

            if (@this.TryGetValue(key, out var result))
                return result;

            var valueToAdd = await asyncFactory().ConfigureAwait(configureAwait);
            @this.Add(key, valueToAdd);
            return valueToAdd;
        }
#endif
    }
}
