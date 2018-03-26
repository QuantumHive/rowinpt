using System;

namespace AlperAslanApps.Core.Contract.Commands
{
    public abstract class UserCommand
    {
        public Guid UserId { get; set; }
    }
}
