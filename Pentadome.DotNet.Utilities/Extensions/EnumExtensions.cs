using System;
using System.Collections.Generic;
using System.Globalization;
using EnumsNET;
using System.Linq;

namespace Pentadome.DotNet.Utilities
{
#if NETCOREAPP
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the index of the current flag in this <typeparamref name="TEnum"/>
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static int GetIndexOfFlag<TEnum>(this TEnum @this)
            where TEnum : struct, Enum, IConvertible
        {
            if (!@this.IsValid())
                throw new ArgumentException("must be a valid enum");
            if (@this.GetFlagCount() != 1)
                throw new ArgumentException("must have 1 and only 1 flag");

            return (int)Math.Log2(@this.ToDouble(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Get the indexes of the flags in this <typeparamref name="TEnum"/>
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<int> GetIndexesOfFlags<TEnum>(this TEnum @this)
           where TEnum : struct, Enum, IConvertible
        {
            if (!@this.IsValid())
                throw new ArgumentException("must be a valid enum");

            return @this.GetFlags().Select(GetIndexOfFlag);
        }
    }
#endif
}
