using RowinPt.Contract.Models;

namespace RowinPt.Api
{
    public interface ISessionManager
    {
        void SignIn(LoginCredentials credentials);
        void SignOut();
    }
}
