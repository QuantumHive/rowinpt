using System;

namespace AlperAslanApps.Core
{
    public interface IEditInfo
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string EditedBy { get; set; }
        DateTime EditedOn { get; set; }
    }
}
