using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Models;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.Account
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IReader<UserModel> _userReader;
        private readonly IEnvironment _environment;

        public SignInCommandHandler(
            IAuthenticator authenticator,
            IReader<UserModel> userReader,
            IEnvironment environment)
        {
            _authenticator = authenticator;
            _userReader = userReader;
            _environment = environment;
        }

        public void Handle(SignInCommand command)
        {
            if (_environment.IsDevelopment && command.Credentials.IsDevelopment())
            {
                var authenticationUser = new AuthenticationUser
                {
                    Id = Guid.NewGuid(),
                    SecurityStamp = Guid.NewGuid(),
                };

                _authenticator.SignIn(authenticationUser);
            }
            else
            {
                var user = _userReader.Entities.Single(u => u.NormalizedEmail == command.NormalizedEmail);
                var authenticationUser = new AuthenticationUser
                {
                    Id = user.Id,
                    SecurityStamp = user.SecurityStamp,
                };

                _authenticator.SignIn(authenticationUser);
            }
        }

    }
}
