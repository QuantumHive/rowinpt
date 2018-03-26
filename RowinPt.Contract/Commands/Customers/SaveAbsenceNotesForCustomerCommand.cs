using System;

namespace RowinPt.Contract.Commands.Customers
{
    public class SaveAbsenceNotesForCustomerCommand
    {
        public SaveAbsenceNotesForCustomerCommand(Guid customerId, string notes)
        {
            CustomerId = customerId;
            Notes = notes;
        }

        public Guid CustomerId { get; }
        public string Notes { get; }
    }
}
