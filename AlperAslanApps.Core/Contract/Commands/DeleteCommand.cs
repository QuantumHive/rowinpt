using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.Contract.Commands
{
    [DebuggerStepThrough]
    public class DeleteCommand<TModel>
        where TModel : class, IIdentifier
    {
        public DeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
