using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class IReadOnlyCollectionExtensions
    {
        /// <summary>
        /// Evaluate whether this <see cref="IReadOnlyCollection{T}"/> contains any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T>(this IReadOnlyCollection<T> @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return @this.Count > 0;
        }

        /// <summary>
        /// Evaluate whether this <see cref="IReadOnlyCollection{T}"/> is <see langword="null"/> or does not contain any data.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<T>(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
            this IReadOnlyCollection<T>? @this) => @this?.Any() ?? false;
    }
}
