using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Customers
{
    internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCommand<Customer>>
    {
        private readonly IReader<CustomerModel> _customerReader;
        private readonly IRepository<AgendaModel> _agendaRepository;
        private readonly ITimeProvider _timeProvider;

        public DeleteCustomerCommandHandler(
            IReader<CustomerModel> customerReader,
            IRepository<AgendaModel> agendaRepository,
            ITimeProvider timeProvider)
        {
            _customerReader = customerReader;
            _agendaRepository = agendaRepository;
            _timeProvider = timeProvider;
        }

        public void Handle(DeleteCommand<Customer> command)
        {
            var today = _timeProvider.Today;

            var agendaIds =
                from agenda in _agendaRepository.Entities
                where agenda.CustomerId == command.Id
                where agenda.ScheduleItem.Date >= today
                select agenda.Id;

            _agendaRepository.RemoveByIds(agendaIds);
            _customerReader.DeleteById(command.Id);
        }
    }
}
