using System;

namespace RowinPt.Contract.Commands.Plan
{
    public class PlanNewScheduleItemForCustomerCommand
    {
        public PlanNewScheduleItemForCustomerCommand(Guid customerId, Guid scheduleItemId)
        {
            CustomerId = customerId;
            ScheduleItemId = scheduleItemId;
        }

        public Guid CustomerId { get; }
        public Guid ScheduleItemId { get; }
    }
}
