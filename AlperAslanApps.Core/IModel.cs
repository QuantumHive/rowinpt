using System;

namespace AlperAslanApps.Core
{
    public interface IModel : IIdentifier, IEditInfo
    {
        bool Active { get; set; }
        Guid CompanyId { get; set; }
    }
}
