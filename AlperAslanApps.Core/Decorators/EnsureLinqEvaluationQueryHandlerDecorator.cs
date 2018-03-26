using System;
using System.Collections.Generic;
using System.Linq;

namespace AlperAslanApps.Core.Decorators
{
    public class EnsureLinqEvaluationQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratee;

        public EnsureLinqEvaluationQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decoratee)
        {
            _decoratee = decoratee;
        }

        public TResult Handle(TQuery query)
        {
            query.ThrowIfNull(nameof(query));

            var result = _decoratee.Handle(query);

            if (IsEnumerable(result.GetType()))
            {
                return Enumerable.ToList((dynamic)result);
            }

            return result;
        }

        private static bool IsEnumerable(Type type) =>
            type.GetInterfaces()
                .Any(t => t.IsGenericType
                          && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
    }
}
