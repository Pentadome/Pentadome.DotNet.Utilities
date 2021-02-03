using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pentadome.DotNet.Utilities
{
    public static class StreamExtensions
    {
        public async static Task<byte[]> ReadAsync(this Stream @this, CancellationToken cancellationToken = default)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var buffer = new byte[@this.Length];
            await @this.ReadAsync(buffer, 0, (int)@this.Length, cancellationToken).ConfigureAwait(false);
            return buffer;
        }
    }
}
