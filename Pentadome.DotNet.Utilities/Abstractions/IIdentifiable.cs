using System;

namespace Pentadome.DotNet.Utilities
{
    public interface IIdentifiable : IIdentifiable<Guid>
    {
    }

    public interface IIdentifiable<T>
    {
        T Id { get; }
    }
}
