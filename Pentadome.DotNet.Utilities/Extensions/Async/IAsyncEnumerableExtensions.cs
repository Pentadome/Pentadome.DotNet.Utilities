#if SUPPORTSNULLATTRIBUTES
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pentadome.DotNet.Utilities
{
    [DebuggerStepThrough]
    public static class IAsyncEnumerableExtensions
    {
        /// <summary>
        /// Asynchronously enumerate the <see cref="IAsyncEnumerable{T}"/> and apply the specified <paramref name="action"/> to each item. <br/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task ForEachAsync<T>(this IAsyncEnumerable<T> @this, Action<T> action)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            if (action is null)
                throw new ArgumentNullException(nameof(action));

            await foreach (var item in @this)
            {
                action(item);
            }
        }

        /// <summary>
        /// Asynchronously enumerate the <see cref="IAsyncEnumerable{TSource}"/> and apply the specified <paramref name="selector"/> to each item. <br/>
        /// Asynchronously yield each outcome in a new <see cref="IAsyncEnumerable{TResult}"/>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IAsyncEnumerable<TSource> @this, Func<TSource, TResult> selector)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return AsyncIterator();

            async IAsyncEnumerable<TResult> AsyncIterator()
            {
                await foreach (var item in @this)
                {
                    yield return selector(item);
                }
            }
        }
    }
}
#endif