using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Evaluate whether a member has a specific custom <typeparamref name="TAttribute"/> applied to it.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCustomAttribute<TAttribute>(this MemberInfo @this)
            where TAttribute : Attribute
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            return Attribute.IsDefined(@this, typeof(TAttribute));
        }

        /// <summary>
        /// Evaluate whether a member has a specific custom <see cref="Attribute"/> applied to it.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCustomAttribute(this MemberInfo @this, Type attributeType)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (attributeType is null)
                throw new ArgumentNullException(nameof(attributeType));
            return Attribute.IsDefined(@this, attributeType);
        }
    }
}
