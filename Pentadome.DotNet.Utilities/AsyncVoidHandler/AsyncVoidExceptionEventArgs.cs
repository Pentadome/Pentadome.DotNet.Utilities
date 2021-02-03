using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pentadome.DotNet.Utilities
{
    public sealed class AsyncVoidExceptionEventArgs : EventArgs
    {
        public AsyncVoidExceptionEventArgs(Task task, Exception exception)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public Task Task { get; }
        public Exception Exception { get; }
    }
}
