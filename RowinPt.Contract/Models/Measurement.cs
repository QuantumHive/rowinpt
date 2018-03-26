using System;
using AlperAslanApps.Core;

namespace RowinPt.Contract.Models
{
    public class Measurement : IIdentifier
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public float? Weight { get; set; }
        public float? FatPercentage { get; set; }

        public float? ShoulderSize { get; set; }
        public float? ArmSize { get; set; }
        public float? BellySize { get; set; }
        public float? WaistSize { get; set; }
        public float? UpperLegSize { get; set; }
    }

    
}
