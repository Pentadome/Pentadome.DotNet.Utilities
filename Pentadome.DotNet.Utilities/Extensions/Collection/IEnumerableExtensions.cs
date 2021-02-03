using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    [DebuggerStepThrough]
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Enumerate through the <see cref="IEnumerable{T}"/> and apply the specific <paramref name="action"/> on each item.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in @this)
            {
                action(item);
            }
        }

        /// <summary>
        /// Invoke <paramref name="selector"/> on each item in this <see cref="IEnumerable{T}"/> and return the results excluding <see langword="null"/> values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, TResult?> selector)
            where TResult : class
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            //An iterator method(a method that contains `yield`) will not validate arguments until the caller begins to enumerate the result items.

            //To ensure that arguments are validated immediately(when the method is called), move
            //the iterator to a separate method(local function).

            return Iterator();

            IEnumerable<TResult> Iterator()
            {
                foreach (var item in @this)
                {
                    var selected = selector(item);
                    if (!(selected is null))
                        yield return selected;
                }
            }
        }

        /// <summary>
        /// Invoke <paramref name="selector"/> on each item in this <see cref="IEnumerable{T}"/> and return the results excluding <see langword="null"/> values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, TResult?> selector)
            where TResult : struct
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            //An iterator method(a method that contains `yield`) will not validate arguments until the caller begins to enumerate the result items.

            //To ensure that arguments are validated immediately(when the method is called), move
            //the iterator to a separate method(local function).

            IEnumerable<TResult> Iterator()
            {
                foreach (var item in @this)
                {
                    var selected = selector(item);
                    if (selected.HasValue)
                        yield return selected.Value;
                }
            }
        }

        /// <summary>
        /// Evaluate whether this <see cref="IEnumerable{T}"/> is <see langword="null"/> or does not yield any data during enumeration.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<T>(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
        this IEnumerable<T>? @this)
        {
            return @this is ICollection<T> @thisCollection ? thisCollection.HasValue() : @this?.Any() ?? false;
        }

        /// <summary>
        /// Concatenate two sequences if both sequences are not <see langword="null"/>  or empty. Otherwise return the sequence that is not <see langword="null"/> or empty. <br/>
        /// If both sequences are <see langword="null"/> or empty, return an empty <see cref="IEnumerable{T}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ConcatNotNull<T>(this IEnumerable<T>? @this, IEnumerable<T>? other)
        {
            if (@this.HasValue())
            {
#if !SUPPORTSNULLATTRIBUTES
    #pragma warning disable CS8603 // Possible null reference return. Reason: NotNullWhenAttribute not available in DotNet Standard.
#endif
                return other.HasValue() ? @this.Concat(other) : @this;
            }
            return other.HasValue() ? other : Enumerable.Empty<T>();
#if !SUPPORTSNULLATTRIBUTES
    #pragma warning restore CS8603 // Possible null reference return.
#endif
        }

        /// <summary>
        /// Get the first item where <paramref name="predicate"/> returns <see langword="true"/>. If <paramref name="predicate"/> never returns <see langword="true"/>, get the first item.
        /// If the <see cref="IEnumerable{T}"></see> is empty, returns the <see langword="default"/> value for <typeparamref name="T"/>.
        /// </summary>
#if SUPPORTSNULLATTRIBUTES
        [return: MaybeNull]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstWhereTrueOrFirstOrDefault<T>(this IEnumerable<T> @this, Func<T, bool> predicate)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (var item in @this)
            {
                if (predicate(item))
                    return item;
            }

            return @this.FirstOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyStringWithValue(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
            this IEnumerable<string?>? @this) =>
            @this?.Any(x => x.HasValue()) ?? false;
    }
}
