using System;

namespace Pentadome.DotNet.Utilities
{
    public sealed class EntityNotSavedException : InvalidOperationException
    {
        public EntityNotSavedException() { }

        public EntityNotSavedException(string? message) : base(message) { }

        public EntityNotSavedException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
