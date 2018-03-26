using System.Diagnostics;

namespace AlperAslanApps.Core.Contract.Commands
{
    [DebuggerStepThrough]
    public class UpdateCommand<TModel>
        where TModel : class, IIdentifier
    {
        public UpdateCommand(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; }
    }
}
