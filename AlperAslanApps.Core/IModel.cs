using System;

namespace AlperAslanApps.Core
{
    public interface IModel : IIdentifier
    {
        bool Active { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string EditedBy { get; set; }
        DateTime EditedOn { get; set; }
    }
}
