using System;

namespace RowinPt.Contract.Commands.Account
{
    public class ResetPasswordCommand
    {
        public ResetPasswordCommand(Guid id, string token, string password)
        {
            Id = id;
            Token = token;
            Password = password;
        }

        public Guid Id { get; }
        public string Token { get; }
        public string Password { get; }
    }
}
