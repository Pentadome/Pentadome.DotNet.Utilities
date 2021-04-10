using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pentadome.DotNet.EntityFrameWorkCore.Utilities
{
    public static class EntityTypeBuilderExtensions
    {
        public static void Properties<T>(this EntityTypeBuilder<T> @this, Expression<Func<T, object>> properties, Action<PropertyBuilder> config)
            where T : class
        {
            if (properties is null)
                throw new ArgumentNullException(nameof(properties));

            var propertyNames = (properties.Body as NewExpression)!.Members.Select(x => x.Name);

            Properties(@this, propertyNames, config);
        }

        public static void Properties(this EntityTypeBuilder @this, Action<PropertyBuilder> config, params string[] propertyNames)
        {
            if (propertyNames is null)
                throw new ArgumentNullException(nameof(propertyNames));

            Properties(@this, propertyNames, config);
        }

        public static void Properties(this EntityTypeBuilder @this, IEnumerable<string> propertyNames, Action<PropertyBuilder> config)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (propertyNames is null)
                throw new ArgumentNullException(nameof(propertyNames));
            if (config is null)
                throw new ArgumentNullException(nameof(config));

            foreach (var propertyName in propertyNames)
            {
                var propertyBuilder = @this.Property(propertyName);
                config(propertyBuilder);
            }
        }
    }
}
