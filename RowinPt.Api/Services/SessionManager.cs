using System;
using AlperAslanApps.Core;
using RowinPt.Contract.Commands.Account;
using RowinPt.Contract.Models;

namespace RowinPt.Api.Services
{
    public class SessionManager : ISessionManager
    {
        private readonly ICommandHandler<SignInCommand> _loginHandler;
        private readonly ICommandHandler<SignOutCommand> _logoutHandler;

        public SessionManager(
            ICommandHandler<SignInCommand> loginHandler,
            ICommandHandler<SignOutCommand> logoutHandler)
        {
            _loginHandler = loginHandler;
            _logoutHandler = logoutHandler;
        }

        public void SignIn(LoginCredentials credentials)
        {
            var command = new SignInCommand(credentials);
            _loginHandler.Handle(command);
        }

        public void SignOut()
        {
            _logoutHandler.Handle(new SignOutCommand());
        }
    }
}
