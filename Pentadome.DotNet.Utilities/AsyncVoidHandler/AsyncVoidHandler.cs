using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pentadome.DotNet.Utilities
{
    public sealed class AsyncVoidHandler
    {
        public event EventHandler<AsyncVoidExceptionEventArgs>? ExceptionThrown;

        public async void RunAsync(Func<Task> asyncFunction)
        {
            if (asyncFunction is null)
                throw new ArgumentNullException(nameof(asyncFunction));

            await RunTaskSafeAsync(asyncFunction()).ConfigureAwait(false);
        }

        public async void RunAsync(Task task)
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            await RunTaskSafeAsync(task).ConfigureAwait(false);
        }

        private async Task RunTaskSafeAsync(Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ExceptionThrown?.Invoke(this, new AsyncVoidExceptionEventArgs(task, new AggregateException("An exception was thrown by an async void function", ex)));
            }
        }
    }
}
