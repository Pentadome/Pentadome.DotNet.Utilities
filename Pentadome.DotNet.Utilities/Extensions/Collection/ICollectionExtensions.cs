using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Pentadome.DotNet.Utilities
{
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Evaluate whether this <see cref="ICollection{T}"/> contains any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T>(this ICollection<T> @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return @this.Count > 0;
        }

        /// <summary>
        /// Evaluate whether this <see cref="ICollection{T}"/> is <see langword="null"/> or does not contain any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<T>(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
        this ICollection<T>? @this) => @this?.Any() ?? false;

        /// <summary>
        /// Evaluate whether this <see cref="ICollection"/> contains any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this ICollection @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return @this.Count > 0;
        }

        /// <summary>
        /// Evaluate whether this <see cref="ICollection"/> is <see langword="null"/> or does not contain any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
        this ICollection? @this) => @this?.Any() ?? false;

        // Some classes implement both ICollection and IReadonlyCollection, this can cause a conflict between choosing extension methods.

        /// <summary>
        /// Evaluate whether this object that implements <see cref="ICollection"/> and <see cref="IReadOnlyCollection{T}"/> contains any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TCollection>(this TCollection @this)
            where TCollection : class, ICollection, IReadOnlyCollection<object> =>
            (@this as ICollection).Any();

        /// <summary>
        /// Evaluate whether this object that implements both <see cref="ICollection"/> and <see cref="IReadOnlyCollection{T}"/>
        /// is <see langword="null"/> or does not contain any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<TCollection>(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
            this TCollection? @this) where TCollection : class, ICollection, IReadOnlyCollection<object> =>
            (@this as ICollection).HasValue();

        /// <summary>
        /// Calls <see cref="List{T}.AddRange(IEnumerable{T})"/> if underlying type is <see cref="List{T}"/>.<br/>
        /// Otherwise, adds an item one by one to the <see cref="ICollection{T}"/>, which might be an expensive operation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ICollection<T> @this, params T[] items)
        {
            AddRange(@this, items as IEnumerable<T>);
        }

        /// <summary>
        /// Calls <see cref="List{T}.AddRange(IEnumerable{T})"/> if underlying type is <see cref="List{T}"/> <br/>
        /// Otherwise, adds an item one by one to the <see cref="ICollection{T}"/>, which might be an expensive operation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ICollection<T> @this, IEnumerable<T> items)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            if (@this is List<T> @thisList)
            {
                thisList.AddRange(items);
                return;
            }
            foreach (var item in items)
            {
                @this.Add(item);
            }
        }
#if !NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task AddItemsFromStreamAsync<T>(this ICollection<T> @this, IAsyncEnumerable<T> itemsStream, bool configureAwait = true)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (itemsStream is null)
                throw new ArgumentNullException(nameof(itemsStream));

            await foreach (var item in itemsStream.ConfigureAwait(configureAwait))
            {
                @this.Add(item);
            }
        }
#endif
    }
}

