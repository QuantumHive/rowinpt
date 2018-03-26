using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AlperAslanApps.Core.Decorators
{
    [DebuggerStepThrough]
    public class ObjectValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _decoratee;

        public ObjectValidationCommandHandlerDecorator(ICommandHandler<TCommand> decoratee)
        {
            _decoratee = decoratee;
        }

        public void Handle(TCommand command)
        {
            command.ThrowIfNull(nameof(command));

            var validationContext = new ValidationContext(command);
            Validator.ValidateObject(command, validationContext, validateAllProperties: true);

            _decoratee.Handle(command);
        }
    }
}
