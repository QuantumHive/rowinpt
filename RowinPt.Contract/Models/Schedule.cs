using AlperAslanApps.Core;
using System;

namespace RowinPt.Contract.Models
{
    public class Schedule : IIdentifier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
    }
}
