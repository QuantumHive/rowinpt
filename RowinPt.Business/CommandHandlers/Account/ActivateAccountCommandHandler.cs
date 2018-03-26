using AlperAslanApps.Core;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.CommandHandlers.Account
{
    internal sealed class ActivateAccountCommandHandler : ICommandHandler<ActivateAccountCommand>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly IPasswordHasher _passwordHasher;

        public ActivateAccountCommandHandler(
            IReader<UserModel> userReader,
            IPasswordHasher passwordHasher)
        {
            _userReader = userReader;
            _passwordHasher = passwordHasher;
        }

        public void Handle(ActivateAccountCommand command)
        {
            var user = _userReader.GetById(command.Activation.Id);
            var hash = _passwordHasher.HashPassword(command.Activation.Password);

            user.PasswordHash = hash;
            user.EmailConfirmed = true;
            user.SecurityStamp = Guid.NewGuid();
        }
    }
}
