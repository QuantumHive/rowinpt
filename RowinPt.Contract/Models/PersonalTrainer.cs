using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using System;

namespace RowinPt.Contract.Models
{
    public class PersonalTrainer : IIdentifier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Sex Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
