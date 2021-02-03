using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class ListExtensions
    {
        /// <summary>
        /// Add multiple <paramref name="items"/> to a <see cref="List{T}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this List<T> @this, params T[] items)
        {
            if (@this is null)
                throw new System.ArgumentNullException(nameof(@this));
            if (items is null)
                throw new System.ArgumentNullException(nameof(items));

            @this.AddRange(items);
        }

        /// <summary>
        /// Add an item to the front of the <see cref="List{T}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Prepend<T>(this List<T> @this,
#if SUPPORTSNULLATTRIBUTES
            [AllowNull]
#endif
        T item)
        {
            if (@this is null)
                throw new System.ArgumentNullException(nameof(@this));

            @this.Insert(0, item!);
        }

        /// <summary>
        /// Add items to the front of the <see cref="List{T}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrependRange<T>(this List<T> @this, params T[] items)
        {
            PrependRange(@this, items as IEnumerable<T>);
        }

        /// <summary>
        /// Add items to the front of the <see cref="List{T}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrependRange<T>(this List<T> @this, IEnumerable<T> items)
        {
            if (@this is null)
                throw new System.ArgumentNullException(nameof(@this));
            if (items is null)
                throw new System.ArgumentNullException(nameof(items));

            @this.InsertRange(0, items);
        }
    }
}
