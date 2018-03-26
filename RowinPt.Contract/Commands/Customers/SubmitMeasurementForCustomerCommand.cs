using RowinPt.Contract.Models;
using System;

namespace RowinPt.Contract.Commands.Customers
{
    public class SubmitMeasurementForCustomerCommand
    {
        public SubmitMeasurementForCustomerCommand(Guid customerId, Measurement measurement)
        {
            CustomerId = customerId;
            Measurement = measurement;
        }

        public Guid CustomerId { get; }
        public Measurement Measurement { get; }
    }
}
