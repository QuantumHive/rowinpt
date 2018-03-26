using System;
using AlperAslanApps.Core;

namespace RowinPt.Contract.Models
{
    public class Location : IIdentifier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
    }
}
