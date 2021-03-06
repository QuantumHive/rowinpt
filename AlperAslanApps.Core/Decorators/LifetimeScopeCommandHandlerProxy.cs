using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.Decorators
{
    [DebuggerStepThrough]
    public class LifetimeScopeCommandHandlerProxy<TCommand> : ICommandHandler<TCommand>
           where TCommand : class
    {
        private readonly IScopeStarter _scopeStarter;
        private readonly Func<ICommandHandler<TCommand>> _decorateeFactory;

        public LifetimeScopeCommandHandlerProxy(
            IScopeStarter scopeStarter,
            Func<ICommandHandler<TCommand>> decorateeFactory)
        {
            _scopeStarter = scopeStarter;
            _decorateeFactory = decorateeFactory;
        }

        public void Handle(TCommand command)
        {
            command.ThrowIfNull(nameof(command));

            using (_scopeStarter.BeginScope())
            {
                ICommandHandler<TCommand> handler = _decorateeFactory.Invoke();
                handler.Handle(command);
            }
        }
    }
}
