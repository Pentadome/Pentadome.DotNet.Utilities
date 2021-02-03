using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Pentadome.DotNet.Utilities
{
    public static class CultureInfoExtensions
    {
#if !NETSTANDARD2_0
        [SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "Comparing a culture insensitive char.")]
#endif
        public static string GetNativeCountryName(this CultureInfo @this)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            // Español (España, alfabetización internacional)
            var indexOfStart = @this.NativeName.IndexOf('(') + 1;
            var indexOfEnd = @this.NativeName.IndexOf(',', indexOfStart) - 1;

            if (indexOfEnd <= 0)
                indexOfEnd = @this.NativeName.IndexOf(')', indexOfStart) - 1;

#if NETSTANDARD2_0
            return @this.NativeName.Substring(indexOfStart, indexOfEnd - indexOfStart);
#else
            return @this.NativeName[indexOfStart..indexOfEnd];
#endif
        }
    }
}
