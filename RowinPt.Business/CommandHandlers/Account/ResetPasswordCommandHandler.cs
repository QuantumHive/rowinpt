using AlperAslanApps.Core;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.CommandHandlers.Account
{
    internal sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordCommandHandler(
            IReader<UserModel> userReader,
            IPasswordHasher passwordHasher)
        {
            _userReader = userReader;
            _passwordHasher = passwordHasher;
        }

        public void Handle(ResetPasswordCommand command)
        {
            var user = _userReader.GetById(command.Id);
            var hash = _passwordHasher.HashPassword(command.Password);

            user.PasswordHash = hash;
            user.SecurityStamp = Guid.NewGuid();
        }
    }
}
