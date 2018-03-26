using AlperAslanApps.Core;
using RowinPt.Contract.Commands.Customers;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Customers
{
    internal sealed class SaveAbsenceNotesForCustomerCommandHandler : ICommandHandler<SaveAbsenceNotesForCustomerCommand>
    {
        private readonly IRepository<AbsenceNotesModel> _absenceNotesRepository;

        public SaveAbsenceNotesForCustomerCommandHandler(
            IRepository<AbsenceNotesModel> absenceNotesRepository)
        {
            _absenceNotesRepository = absenceNotesRepository;
        }

        public void Handle(SaveAbsenceNotesForCustomerCommand command)
        {
            var notes = _absenceNotesRepository.Entities.SingleOrDefault(a => a.CustomerId == command.CustomerId);

            if (notes == null)
            {
                _absenceNotesRepository.Add(new AbsenceNotesModel
                {
                    Notes = command.Notes,
                    CustomerId = command.CustomerId,
                });
            }
            else
            {
                notes.Notes = command.Notes;
            }
        }
    }
}
