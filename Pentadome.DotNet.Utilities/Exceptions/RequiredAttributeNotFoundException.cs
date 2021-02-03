using System;
using System.Reflection;

namespace Pentadome.DotNet.Utilities
{
    public sealed class RequiredAttributeNotFoundException : TargetException
    {
        public RequiredAttributeNotFoundException() { }

        public RequiredAttributeNotFoundException(string? message) : base(message) { }

        public RequiredAttributeNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
