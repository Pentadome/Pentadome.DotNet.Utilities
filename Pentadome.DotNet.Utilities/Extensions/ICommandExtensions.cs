using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Pentadome.DotNet.Utilities
{
    public static class ICommandExtensions
    {
        /// <summary>
        /// Equivalent to <see cref="ICommand.Execute(object)"/> with <see langword="null"/> as argument.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Execute(this ICommand @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            @this.Execute(null);
        }
    }
}
