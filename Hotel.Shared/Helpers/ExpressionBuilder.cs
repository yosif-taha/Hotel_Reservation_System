using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Shared.Helpers
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>>? BuildFilterExpression<T, TFilter>(TFilter filterDto)
        {
            if (filterDto == null)
                return null;

            // x =>
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combined = null;

            foreach (var prop in typeof(TFilter).GetProperties())
            {
                var value = prop.GetValue(filterDto);
                if (value == null) continue;

                var entityProp = typeof(T).GetProperty(prop.Name);
                if (entityProp == null) continue;

                //X.Property
                var left = Expression.Property(parameter, entityProp);
                // constant value
                var constant = Expression.Constant(value);

                Expression condition;

                if (entityProp.PropertyType == typeof(string))
                {
                    var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
                    condition = Expression.Call(left, containsMethod, constant);
                }
                else
                {
                    condition = Expression.Equal(left, constant);
                }

                combined = combined == null ? condition : Expression.OrElse(combined, condition);
            }

            return combined != null
                ? Expression.Lambda<Func<T, bool>>(combined, parameter)
                : null;
        }
    }
}
