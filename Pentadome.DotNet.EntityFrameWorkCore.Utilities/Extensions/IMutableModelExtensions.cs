using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pentadome.DotNet.Utilities;

namespace Pentadome.DotNet.EntityFrameWorkCore.Utilities.Extensions
{
    public static class IMutableModelExtensions
    {
        public static IMutableModel ForAllProperties<T>(this IMutableModel @this, Action<IMutableProperty> configurator)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (configurator is null)
                throw new ArgumentNullException(nameof(configurator));

            var properties = @this
                .GetEntityTypes()
                .SelectMany(x =>
                    x.GetProperties()
                    .Where(x => x.ClrType.Equals(typeof(T))))
                .Distinct();

            properties.ForEach(configurator);

            return @this;
        }

        public static IMutableModel SetQueryFilterForInterfaceOrBase<T>(this IMutableModel @this, Expression<Func<T, bool>> filterExpression)
            where T : class
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));
            if (filterExpression is null)
                throw new ArgumentNullException(nameof(filterExpression));

            var typesThatImplementInterFace = @this.GetEntityTypes().Where(x => x.ClrType?.IsAssignableTo<T>() ?? false);
            var parameterName = filterExpression.Parameters[0].Name;
            var bodyExpression = filterExpression.Body;

            foreach (var entityType in typesThatImplementInterFace)
            {
                var newParameterExpression = Expression.Parameter(entityType.ClrType, parameterName);
                var newFilterExpression = Expression.Lambda(bodyExpression, newParameterExpression);
                entityType.SetQueryFilter(newFilterExpression);
            }

            return @this;
        }
    }
}
