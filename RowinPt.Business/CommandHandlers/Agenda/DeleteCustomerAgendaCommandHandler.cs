using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Agenda
{
    internal sealed class DeleteCustomerAgendaCommandHandler : ICommandHandler<DeleteCommand<CustomerAgenda>>
    {
        private readonly IReader<AgendaModel> _agendaReader;

        public DeleteCustomerAgendaCommandHandler(
            IReader<AgendaModel> agendaReader)
        {
            _agendaReader = agendaReader;
        }

        public void Handle(DeleteCommand<CustomerAgenda> command)
        {
            _agendaReader.DeleteById(command.Id);
        }
    }
}
