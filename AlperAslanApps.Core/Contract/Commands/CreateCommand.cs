using System.Diagnostics;

namespace AlperAslanApps.Core.Contract.Commands
{
    [DebuggerStepThrough]
    public class CreateCommand<TModel>
        where TModel : class, IIdentifier
    {
        public CreateCommand(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; }
    }
}
