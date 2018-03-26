using AlperAslanApps.Core;
using RowinPt.Contract.Models;
using System;

namespace RowinPt.Contract.Queries.Users
{
    public class GetUserInformationQuery : IQuery<UserInformation>
    {
        public GetUserInformationQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
