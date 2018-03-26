using AlperAslanApps.Core.Contract.Models;

namespace AlperAslanApps.Core
{
    public interface IAuthenticator
    {
        void SignIn(AuthenticationUser user);
        void SignOut();
    }
}
