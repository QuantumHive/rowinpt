using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AlperAslanApps.Core.EntityFrameworkCore.Decorators
{
    [DebuggerStepThrough]
    public class SaveChangesCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
           where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _decoratee;
        private readonly DbContext _databaseContext;
        private readonly IEditInfoHandler _editInfoHandler;

        public SaveChangesCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            DbContext databaseContext,
            IEditInfoHandler editInfoHandler)
        {
            _decoratee = decoratee;
            _databaseContext = databaseContext;
            _editInfoHandler = editInfoHandler;
        }

        public void Handle(TCommand command)
        {
            command.ThrowIfNull(nameof(command));

            _decoratee.Handle(command);
            _editInfoHandler.Track();
            _databaseContext.SaveChanges();
        }
    }
}
