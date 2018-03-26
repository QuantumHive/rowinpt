using AlperAslanApps.Core;
using RowinPt.Contract.Commands.Account;

namespace RowinPt.Business.CommandHandlers.Account
{
    internal sealed class SignOutCommandHandler : ICommandHandler<SignOutCommand>
    {
        private readonly IAuthenticator _authenticator;

        public SignOutCommandHandler(
            IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public void Handle(SignOutCommand command)
        {
            _authenticator.SignOut();
        }
    }
}
