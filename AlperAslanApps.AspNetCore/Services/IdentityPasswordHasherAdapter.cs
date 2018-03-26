using AlperAslanApps.Core;
using Microsoft.AspNetCore.Identity;

namespace AlperAslanApps.AspNetCore.Services
{
    public class User { }
    public class IdentityPasswordHasherAdapter : IPasswordHasher
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public IdentityPasswordHasherAdapter(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(string password) => _passwordHasher.HashPassword(null, password);

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
