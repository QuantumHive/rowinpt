using AlperAslanApps.Core.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace AlperAslanApps.Core.Decorators
{
    [DebuggerStepThrough]
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _decoratee;
        private readonly IValidator<TCommand> _validator;

        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            IValidator<TCommand> validator)
        {
            _decoratee = decoratee;
            _validator = validator;
        }

        public void Handle(TCommand command)
        {
            command.ThrowIfNull(nameof(command));

            Validate(command);

            _decoratee.Handle(command);
        }

        private void Validate(TCommand command)
        {
            var validationExceptions = GetValidationExceptions(command).ToArray();

            if (validationExceptions.Any())
            {
                throw new AggregateValidationException(validationExceptions);
            }
        }

        private IEnumerable<ValidationException> GetValidationExceptions(TCommand command) =>
            from validationResult in _validator.Validate(command)
            select new ValidationException(string.Empty, null, validationResult);
    }
}
