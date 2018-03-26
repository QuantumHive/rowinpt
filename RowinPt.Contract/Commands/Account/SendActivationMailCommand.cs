using System;

namespace RowinPt.Contract.Commands.Account
{
    public class SendActivationMailCommand
    {
        public SendActivationMailCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
