using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class IFormattableExtensions
    {
        /// <summary>
        /// Equivalent to <see cref="IFormattable.ToString(string, IFormatProvider)"/> with <see cref="IFormatProvider"/> = <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToInvariantString<T>(this T @this, string? format = null) where T : IFormattable
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            return @this.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
