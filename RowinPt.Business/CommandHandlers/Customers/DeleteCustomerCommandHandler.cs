using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.Customers
{
    internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCommand<Customer>>
    {
        private readonly IReader<CustomerModel> _customerReader;

        public DeleteCustomerCommandHandler(
            IReader<CustomerModel> customerReader)
        {
            _customerReader = customerReader;
        }

        public void Handle(DeleteCommand<Customer> command)
        {
            _customerReader.DeleteById(command.Id);
        }
    }
}
