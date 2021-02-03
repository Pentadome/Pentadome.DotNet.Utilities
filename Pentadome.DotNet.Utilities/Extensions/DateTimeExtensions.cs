using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    [DebuggerStepThrough]
    public static class DateTimeExtensions
    {
        /// <summary>
        /// ISO 8601 <br/>
        /// "yyyy'-'MM'-'dd'T'HH':'mm':'ss".
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToIsoString(this DateTime @this) => @this.ToInvariantString("s");

        /// <summary>
        /// ISO 8601 <br/>
        /// "yyyy'-'MM'-'dd'T'HH':'mm':'ss".
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToIsoString(this DateTimeOffset @this) => @this.ToInvariantString("s");

        /// <summary>
        /// ISO 8601 <br/>
        /// "yyyy'-'MM'-'dd'T'HH':'mm':'ss'+'zzz".
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToIsoStringOffset(this DateTimeOffset @this) => @this.ToInvariantString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'+'zzz");
    }
}
