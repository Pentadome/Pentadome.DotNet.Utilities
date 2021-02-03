using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static partial class Check
    {
#if SUPPORTSNULLATTRIBUTES
        [DoesNotReturn]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowArgumentOutOfRangeException<TNumber>(string paramName, TNumber argument, TNumber minimum, TNumber maximum)
            where TNumber : unmanaged
        {
            throw new ArgumentOutOfRangeException(paramName, argument, $"Must be between {minimum} and {maximum}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentHasValidRange(double argument, string paramName, double minimum = 0, double maximum = double.MaxValue)
        {
            if (argument < minimum || argument > maximum)
            {
                ThrowArgumentOutOfRangeException(paramName, argument, minimum, maximum);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentHasValidRange(int argument, string paramName, int minimum = 0, int maximum = int.MaxValue)
        {
            if (argument < minimum || argument > maximum)
            {
                ThrowArgumentOutOfRangeException(paramName, argument, minimum, maximum);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentHasValidRange(long argument, string paramName, long minimum = 0, long maximum = long.MaxValue)
        {
            if (argument < minimum || argument > maximum)
            {
                ThrowArgumentOutOfRangeException(paramName, argument, minimum, maximum);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentHasValidRange(float argument, string paramName, float minimum = 0, float maximum = float.MaxValue)
        {
            if (argument < minimum || argument > maximum)
            {
                ThrowArgumentOutOfRangeException(paramName, argument, minimum, maximum);
            }
        }
    }
}
