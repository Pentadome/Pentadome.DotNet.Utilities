using System;

namespace Pentadome.DotNet.Utilities
{
    public sealed class InvalidAttributeUsageException : InvalidOperationException
    {
        public InvalidAttributeUsageException()
        {
        }

        public InvalidAttributeUsageException(string? message) : base(message)
        {
        }

        public InvalidAttributeUsageException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
