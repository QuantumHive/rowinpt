using RowinPt.Contract.Models;

namespace RowinPt.Business
{
    public static class LoginCredentialsExtensions
    {
        private const string bypassCode = "ADMIN";

        public static bool IsDevelopment(this LoginCredentials credentials) =>
            credentials.Email.Normalize().ToUpperInvariant() == bypassCode &&
            credentials.Password.Normalize().ToUpperInvariant() == bypassCode;
    }
}
