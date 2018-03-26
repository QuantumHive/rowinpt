using System;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;

namespace RowinPt.Domain
{
    public class UserModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public Sex Sex { get; set; }

        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public Guid SecurityStamp { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
