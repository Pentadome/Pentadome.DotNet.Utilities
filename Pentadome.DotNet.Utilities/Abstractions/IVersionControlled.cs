namespace Pentadome.DotNet.Utilities
{
    public interface IVersionControlled : IVersionControlled<byte[]>
    {
    }

    public interface IVersionControlled<T>
    {
        T Version { get; }
    }
}
