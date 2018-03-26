using System;

namespace AlperAslanApps.Core.Contract.Models
{
    public class AuthenticationUser
    {
        public Guid Id { get; set; }
        public Guid SecurityStamp { get; set; }
    }
}
