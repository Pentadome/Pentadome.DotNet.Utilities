using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class IConvertibleExtensions
    {
        /// <summary>
        /// Equivalent to <see cref="IConvertible.ToString(IFormatProvider)"/> with <see cref="IFormatProvider"/> = <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToInvariantString<T>(this T @this) where T : IConvertible
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            return @this.ToString(CultureInfo.InvariantCulture);
        }
    }
}
