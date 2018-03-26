using AlperAslanApps.Core.Contract.Commands;

namespace RowinPt.Contract.Commands.Account
{
    public class ChangePasswordCommand : UserCommand
    {
        public ChangePasswordCommand(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string OldPassword { get; }
        public string NewPassword { get; }
    }
}
