using System;
using System.Collections.Generic;
using System.Linq;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using Microsoft.AspNetCore.DataProtection;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;

namespace RowinPt.Business.Validators.Account
{
    internal sealed class AccountActivationErrors : IValidator<ActivateAccountCommand>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly IDataProtector _dataProtector;
        private readonly ITokenProvider<UserModel> _tokenProvider;

        public AccountActivationErrors(
            IReader<UserModel> userReader,
            IDataProtector dataProtector,
            ITokenProvider<UserModel> tokenProvider)
        {
            _userReader = userReader;
            _dataProtector = dataProtector;
            _tokenProvider = tokenProvider;
        }

        public IEnumerable<ValidationObject> Validate(ActivateAccountCommand instance)
        {
            var hasErrors = false;
            var user = _userReader.Entities.SingleOrDefault(u => u.Id == instance.Activation.Id);

            if (user == null)
            {
                hasErrors = true;
            }
            else
            {
                hasErrors |= user.EmailConfirmed;
                hasErrors |= !IsTokenValid(user, instance.Activation.Token);
            }

            if (hasErrors)
            {
                yield return new ValidationObject
                {
                    Message = "Account activation error"
                };
            }
        }

        private bool IsTokenValid(UserModel user, string token)
        {
            var protectedData = Convert.FromBase64String(token);
            var unprotectedData = _dataProtector.Unprotect(protectedData);
            var isTokenValid = _tokenProvider.Validate(unprotectedData, user, "AccountActivation");

            return isTokenValid;
        }
    }
}
