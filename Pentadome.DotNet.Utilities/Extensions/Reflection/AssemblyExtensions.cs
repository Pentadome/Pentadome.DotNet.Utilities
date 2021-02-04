using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Pentadome.DotNet.Utilities
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetExportedTypesSkipExceptions(this Assembly @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            try
            {
                return @this.GetExportedTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types?.Where(x => !(x is null)) ?? Enumerable.Empty<Type>();
            }
        }
    }
}
