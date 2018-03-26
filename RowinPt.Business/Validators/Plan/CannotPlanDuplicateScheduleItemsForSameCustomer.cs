using System.Collections.Generic;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Commands.Plan;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.Validators.Plan
{
    internal sealed class CannotPlanDuplicateScheduleItemsForSameCustomer : IValidator<PlanNewScheduleItemForCustomerCommand>
    {
        private readonly IReader<AgendaModel> _agendaReader;

        public CannotPlanDuplicateScheduleItemsForSameCustomer(
            IReader<AgendaModel> agendaReader)
        {
            _agendaReader = agendaReader;
        }

        public IEnumerable<ValidationObject> Validate(PlanNewScheduleItemForCustomerCommand instance)
        {
            var scheduledInAgenda =
                from agenda in _agendaReader.Entities
                where agenda.ScheduleItemId == instance.ScheduleItemId
                where agenda.CustomerId == instance.CustomerId
                select agenda.Id;

            if (scheduledInAgenda.Any())
            {
                yield return new ValidationObject
                {
                    Message = "Je hebt je al ingepland voor deze les op de gekozen datum en tijd"
                };
            }
        }
    }
}
