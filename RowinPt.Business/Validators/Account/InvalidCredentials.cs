using System.Collections.Generic;
using System.Linq;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;

namespace RowinPt.Business.Validators.Account
{
    internal sealed class InvalidCredentials : IValidator<SignInCommand>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEnvironment _environment;

        public InvalidCredentials(
            IReader<UserModel> userReader,
            IPasswordHasher passwordHasher,
            IEnvironment environment)
        {
            _userReader = userReader;
            _passwordHasher = passwordHasher;
            _environment = environment;
        }

        public IEnumerable<ValidationObject> Validate(SignInCommand instance)
        {
            var user = _userReader.Entities.SingleOrDefault(u => u.NormalizedEmail == instance.NormalizedEmail);

            var invalid = false;

            if (user == null)
            {
                invalid = true;
            }
            else
            {
                if (!user.EmailConfirmed)
                {
                    invalid = true;
                }
                else
                {
                    var isPasswordValid = _passwordHasher.VerifyHashedPassword(user.PasswordHash, instance.Credentials.Password);
                    if (!isPasswordValid)
                    {
                        invalid = true;
                    }
                }
            }

            if (_environment.IsDevelopment && instance.Credentials.IsDevelopment())
            {
                invalid = false;
            }

            // we don't want to inform a potential intruder why the credentials are invalid
            if (invalid)
            {
                yield return new ValidationObject
                {
                    Message = $"Invalid credentials for {instance.Credentials.Email}",
                };
            }
        }
    }
}
