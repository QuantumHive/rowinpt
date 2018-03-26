using RowinPt.Contract.Models;

namespace RowinPt.Contract.Commands.Account
{
    public class SignInCommand
    {
        public SignInCommand(LoginCredentials credentials)
        {
            Credentials = credentials;
        }

        public LoginCredentials Credentials { get; }

        public string NormalizedEmail => Credentials.Email.Normalize().ToUpperInvariant();
    }
}
