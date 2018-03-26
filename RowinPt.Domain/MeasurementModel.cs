using AlperAslanApps.Core;
using System;

namespace RowinPt.Domain
{
    public class MeasurementModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }

        public DateTime Date { get; set; }

        public float? Weight { get; set; }
        public float? FatPercentage { get; set; }
        public float? ShoulderSize { get; set; }
        public float? ArmSize { get; set; }
        public float? BellySize { get; set; }
        public float? WaistSize { get; set; }
        public float? UpperLegSize { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        
    }
}
