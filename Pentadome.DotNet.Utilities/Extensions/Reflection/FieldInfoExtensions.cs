using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Pentadome.DotNet.Utilities.Extensions.Reflection
{
    public static class FieldInfoExtensions
    {
#if SUPPORTSNULLATTRIBUTES
        [return: MaybeNull]
#endif
        public static T GetValue<T>(this FieldInfo @this, object? instance)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return (T)@this.GetValue(instance);
        }

        public static object? GetStaticValue(this FieldInfo @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return @this.GetValue(null)!;
        }
#if SUPPORTSNULLATTRIBUTES
        [return: MaybeNull]
#endif
        public static T GetStaticValue<T>(this FieldInfo @this)
        {
            return (T)@this.GetStaticValue();
        }
    }
}
