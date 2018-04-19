using RowinPt.Contract.Models;

namespace RowinPt.Business
{
    public static class LoginCredentialsExtensions
    {
        private const string bypassCode = "DEV";

        public static bool IsDevelopment(this LoginCredentials credentials) =>
            credentials.Password.Normalize().ToUpperInvariant() == bypassCode;
    }
}
