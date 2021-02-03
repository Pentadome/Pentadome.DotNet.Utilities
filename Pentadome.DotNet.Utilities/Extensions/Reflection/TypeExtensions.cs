using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether an instance of a specified type can be assigned to a variable of the current type.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAssignableFrom<TType>(this Type @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            return @this.IsAssignableFrom(typeof(TType));
        }

        /// <summary>
        /// Evaluate whether this <see cref="Type"/> is derived from <typeparamref name="TType"/> or implements interface <typeparamref name="TType"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAssignableTo<TType>(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
        this Type? @this) => typeof(TType).IsAssignableFrom(@this);

        /// <summary>
        /// Evaluate whether this <see cref="Type"/> is derived from <paramref name="otherType"/> or implements interface <paramref name="otherType"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAssignableTo(
#if SUPPORTSNULLATTRIBUTES
            [NotNullWhen(true)]
#endif
        this Type? @this, Type otherType)
        {
            if (otherType is null)
                throw new ArgumentNullException(nameof(otherType));
            return otherType.IsAssignableFrom(@this);
        }
    }
}
