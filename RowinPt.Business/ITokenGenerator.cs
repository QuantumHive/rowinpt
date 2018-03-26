using RowinPt.Domain;

namespace RowinPt.Business
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserModel user, string purpose);
        bool IsTokenValid(UserModel user, string token, string purpose);
    }
}
