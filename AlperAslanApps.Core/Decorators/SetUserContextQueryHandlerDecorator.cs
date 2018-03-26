using AlperAslanApps.Core.Contract.Queries;
using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.Decorators
{
    [DebuggerStepThrough]
    public class SetUserContextQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratee;
        private readonly IUserContext _userContext;

        public SetUserContextQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decoratee,
            IUserContext userContext)
        {
            _decoratee = decoratee;
            _userContext = userContext;
        }

        public TResult Handle(TQuery query)
        {
            if (query is UserQuery userQuery)
            {
                var userId = _userContext.GetId();

                if (userId == Guid.Empty)
                {
                    throw new UnauthorizedAccessException(
                        $"Cannot set the UserId of derived query '{query.GetType().Name}' because no usercontext has been set.");
                }

                userQuery.UserId = userId;
            }

            return _decoratee.Handle(query);
        }
    }
}
