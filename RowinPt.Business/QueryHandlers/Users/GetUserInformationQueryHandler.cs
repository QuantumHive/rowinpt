using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Users;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.Users
{
    internal sealed class GetUserInformationQueryHandler : IQueryHandler<GetUserInformationQuery, UserInformation>
    {
        private readonly IReader<UserModel> _userReader;

        public GetUserInformationQueryHandler(
            IReader<UserModel> userReader)
        {
            _userReader = userReader;
        }

        public UserInformation Handle(GetUserInformationQuery query)
        {
            var user = _userReader.GetById(query.UserId);

            var name = user.Name.Split().First();

            var userInformation = new UserInformation
            {
                Email = user.Email,
                Name = name,
            };

            if(user is PersonalTrainerModel trainer)
            {
                userInformation.IsAdmin = trainer.Admin;
            }

            return userInformation;
        }
    }
}
