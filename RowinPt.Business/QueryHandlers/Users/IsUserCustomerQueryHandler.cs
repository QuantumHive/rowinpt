using AlperAslanApps.Core;
using RowinPt.Contract.Queries.Users;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.QueryHandlers.Users
{
    internal sealed class IsUserCustomerQueryHandler : IQueryHandler<IsUserCustomerQuery, bool>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly IEnvironment _environment;

        public IsUserCustomerQueryHandler(
            IReader<UserModel> userReader,
            IEnvironment environment)
        {
            _userReader = userReader;
            _environment = environment;
        }

        public bool Handle(IsUserCustomerQuery query)
        {
            try
            {
                var user = _userReader.GetById(query.UserId);
                return user is CustomerModel;
            }
            catch (InvalidOperationException)
            {
                //when logged in with bypass code a random id is generated
                if (_environment.IsDevelopment)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
