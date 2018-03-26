namespace RowinPt.Contract.Commands.Account
{
    public class RequestPasswordResetCommand
    {
        public RequestPasswordResetCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public string NormalizedEmail => Email.Normalize().ToUpperInvariant();
    }
}
