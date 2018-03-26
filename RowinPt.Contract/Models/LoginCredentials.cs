namespace RowinPt.Contract.Models
{
    public class LoginCredentials
    {
        public LoginCredentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
