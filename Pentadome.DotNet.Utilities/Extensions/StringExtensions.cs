using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Pentadome.DotNet.Utilities
{
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveWhiteSpace(this string @this, string replaceWith = "") =>
            RegexConstants.WhiteSpace.Replace(@this, replaceWith);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder ToMutableString(this string? @this) => new StringBuilder(@this);

#if NETSTANDARD2_0

        // Exists in DotNet Core but not in DotNet Standard 2.0 for some reason...
        // https://docs.microsoft.com/en-us/dotnet/api/system.string.contains?view=netstandard-2.0
        // vs
        // https://docs.microsoft.com/en-us/dotnet/api/system.string.contains?view=netcore-3.1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this string @this, string toSearch, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (toSearch is null)
                throw new ArgumentNullException(nameof(toSearch));

            return @this.IndexOf(toSearch, stringComparison) >= 0;
        }
#endif
    }
}
