using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pentadome.DotNet.Utilities
{
    [DebuggerStepThrough]
    public static class TaskExtensions
    {
        /// <summary>
        /// Blocks the current <see cref="ExecutionContext"/> until the <see cref="Task{T}"/> is completed. <br/>
        /// Making it similar in use as a synchronous function, but not exactly the same.
        /// </summary>
        /// <remarks>
        /// Usage should be avoided as sync-over-async is an anti-pattern. See <c>https://makolyte.com/fixing-the-sync-over-async-antipattern/</c>.
        /// </remarks>
        /// <returns>
        /// The result of the <see cref="Task{T}"/>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AsSync<T>(this Task<T> @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return @this.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Blocks the current <see cref="ExecutionContext"/> until the <see cref="Task"/> is completed. <br/>
        /// Making it similar in use as a synchronous function, but not exactly the same.
        /// </summary>
        /// <remarks>
        /// Usage should be avoided as sync-over-async is an anti-pattern. See <c>https://makolyte.com/fixing-the-sync-over-async-antipattern/</c>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AsSync(this Task @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            @this.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Async void is intentional here. This provides a way
        /// to call an async method from a constructor or a non-async function while
        /// communicating intent to fire and forget, and allow
        /// handling of exceptions.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="onException"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void SafeFireAndForget(
            this Task task,
            Action<Exception>? onException = null
            )
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            try
            {
                await task.ConfigureAwait(false);
            }
            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }
    }
}
