using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#if SUPPORTSNULLATTRIBUTES
namespace Pentadome.DotNet.Utilities
{
    [DebuggerStepThrough]
    public static class ValueTaskExtensions
    {
        /// <summary>
        /// Blocks the current <see cref="ExecutionContext"/> until the <see cref="ValueTask{T}"/> is complete. <br/>
        /// Making it similar in use as a synchronous function, but not exactly the same.
        /// </summary>
        /// <remarks>
        /// Usage should be avoided as sync-over-async is an anti-pattern. See <c>https://makolyte.com/fixing-the-sync-over-async-antipattern/</c>.
        /// </remarks>
        /// <returns>
        /// The result of the <see cref="ValueTask{T}"/>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AsSync<T>(this ValueTask<T> @this) => @this.GetAwaiter().GetResult();

        /// <summary>
        /// Blocks the current <see cref="ExecutionContext"/> until the <see cref="ValueTask"/> is complete. <br/>
        /// Making it similar in use as a synchronous function, but not exactly the same.
        /// </summary>
        /// <remarks>
        /// Usage should be avoided as sync-over-async is an anti-pattern. See <c>https://makolyte.com/fixing-the-sync-over-async-antipattern/</c>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AsSync(this ValueTask @this) => @this.GetAwaiter().GetResult();
    }
}
#endif