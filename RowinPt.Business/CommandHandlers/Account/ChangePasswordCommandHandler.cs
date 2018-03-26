using AlperAslanApps.Core;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.CommandHandlers.Account
{
    internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IReader<UserModel> _userReader;

        public ChangePasswordCommandHandler(
            IPasswordHasher passwordHasher,
            IReader<UserModel> userReader)
        {
            _passwordHasher = passwordHasher;
            _userReader = userReader;
        }

        public void Handle(ChangePasswordCommand command)
        {
            var passwordHash = _passwordHasher.HashPassword(command.NewPassword);

            var currentUser = _userReader.GetById(command.UserId);

            currentUser.PasswordHash = passwordHash;
            currentUser.SecurityStamp = Guid.NewGuid();
        }
    }
}
