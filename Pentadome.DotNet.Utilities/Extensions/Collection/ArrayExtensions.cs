using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class ArrayExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if SUPPORTSNULLATTRIBUTES
        [return: MaybeNull]
#endif
        public static T FirstOfArrayOrDefault<T>(this T[] @this, Predicate<T>? predicate = null)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return predicate is null ? @this[0] : Array.Find(@this, predicate);
        }
    }
}
