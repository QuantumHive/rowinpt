using AlperAslanApps.Core.Contract.Commands;
using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.Decorators
{
    [DebuggerStepThrough]
    public class SetUserContextCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _decoratee;
        private readonly IUserContext _userContext;

        public SetUserContextCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            IUserContext userContext)
        {
            _decoratee = decoratee;
            _userContext = userContext;
        }

        public void Handle(TCommand command)
        {
            if(command is UserCommand userCommand)
            {
                var userId = _userContext.GetId();

                if(userId == Guid.Empty)
                {
                    throw new UnauthorizedAccessException(
                        $"Cannot set the UserId of derived command '{command.GetType().Name}' because no usercontext has been set.");
                }

                userCommand.UserId = userId;
            }

            _decoratee.Handle(command);
        }
    }
}
