using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System.Collections.Generic;

namespace RowinPt.Business.Validators.Account
{
    internal sealed class CannotChangePasswordBecauseOfInvalidOldPassword : IValidator<ChangePasswordCommand>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IReader<UserModel> _userReader;

        public CannotChangePasswordBecauseOfInvalidOldPassword(
            IPasswordHasher passwordHasher,
            IReader<UserModel> userReader)
        {
            _passwordHasher = passwordHasher;
            _userReader = userReader;
        }

        public IEnumerable<ValidationObject> Validate(ChangePasswordCommand instance)
        {
            var currentUser = _userReader.GetById(instance.UserId);
            var userPasswordHash = currentUser.PasswordHash;
            var isValid = _passwordHasher.VerifyHashedPassword(userPasswordHash, instance.OldPassword);

            if (!isValid)
            {
                yield return new ValidationObject
                {
                    Message = "Oude wachtwoord is onjuist"
                };
            }
        }
    }
}
