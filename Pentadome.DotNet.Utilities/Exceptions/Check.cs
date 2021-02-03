
using EnumsNET;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    [DebuggerStepThrough]
    public static partial class Check
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ArgumentNotNull<T>(
#if SUPPORTSNULLATTRIBUTES
            [NotNull, AllowNull]
#endif
            T argument, string paramName) where T : class
        {
            if (argument is null)
                throw new ArgumentNullException(paramName);

            return argument;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string StringArgumentNotNullOrWhiteSpace(
#if SUPPORTSNULLATTRIBUTES
            [NotNull, AllowNull]
#endif
            string argument, string paramName)
        {
            if (argument is null)
                throw new ArgumentNullException(paramName);

            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentException("This argument can not be a null, empty or whitespace string", paramName);

            return argument;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string StringArgumentNotNullOrEmpty(
#if SUPPORTSNULLATTRIBUTES
            [NotNull, AllowNull]
#endif
        string argument, string paramName)
        {
            if (argument is null)
                throw new ArgumentNullException(paramName);

            if (string.IsNullOrEmpty(argument))
                throw new ArgumentException("This argument can not be a null or empty string", paramName);

            return argument;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnumArgumentIsValid<T>(T argument, string paramName)
            where T : struct, Enum, IConvertible
        {
            if (argument.IsValid())
                return argument;

            throw new InvalidEnumArgumentException(paramName, argument.ToInt32(null), typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetArgumentTyped<T>(
#if SUPPORTSNULLATTRIBUTES
            [NotNull]
#endif
            object? argument, string paramName) where T : notnull
        {
            if (argument is T desired)
                return desired;

            throw new ArgumentException(
                $"Expected {paramName} to be of type {typeof(T).FullName}, got {argument?.GetType().FullName ?? "null"} instead.",
                paramName);
        }
    }
}