using System;

namespace Pentadome.DotNet.Utilities
{
    public interface ISoftDeletable
    {
        DateTimeOffset? SoftDeleted { get; }
    }
}
