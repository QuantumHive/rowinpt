using System;
using AlperAslanApps.Core;
using Microsoft.AspNetCore.DataProtection;
using RowinPt.Domain;

namespace RowinPt.Business.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly ITokenProvider<UserModel> _tokenProvider;
        private readonly IDataProtector _dataProtector;

        public TokenGenerator(
            ITokenProvider<UserModel> tokenProvider,
            IDataProtector dataProtector)
        {
            _tokenProvider = tokenProvider;
            _dataProtector = dataProtector;
        }

        public string GenerateToken(UserModel user, string purpose)
        {
            var unprotectedToken = _tokenProvider.Generate(user, purpose);
            var protectedToken = _dataProtector.Protect(unprotectedToken);
            var token = Convert.ToBase64String(protectedToken);
            return token;
        }

        public bool IsTokenValid(UserModel user, string token, string purpose)
        {
            var protectedData = Convert.FromBase64String(token);
            var unprotectedData = _dataProtector.Unprotect(protectedData);
            var isTokenValid = _tokenProvider.Validate(unprotectedData, user, purpose);

            return isTokenValid;
        }
    }
}
