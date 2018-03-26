using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.Validators.Account
{
    internal sealed class PasswordResetErrors : IValidator<ResetPasswordCommand>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly ITokenGenerator _tokenGenerator;

        public PasswordResetErrors(
            IReader<UserModel> userReader,
            ITokenGenerator tokenGenerator,
            ITokenProvider<UserModel> tokenProvider)
        {
            _userReader = userReader;
            _tokenGenerator = tokenGenerator;
        }

        public IEnumerable<ValidationObject> Validate(ResetPasswordCommand instance)
        {
            var hasErrors = false;
            var user = _userReader.Entities.SingleOrDefault(u => u.Id == instance.Id);

            if (user == null)
            {
                hasErrors = true;
            }
            else
            {
                hasErrors |= !user.EmailConfirmed;
                hasErrors |= !_tokenGenerator.IsTokenValid(user, instance.Token, "ResetPassword");
            }

            if (hasErrors)
            {
                yield return new ValidationObject
                {
                    Message = "Password reset error"
                };
            }
        }
    }
}
