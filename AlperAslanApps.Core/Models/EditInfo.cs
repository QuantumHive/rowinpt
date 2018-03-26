using System;
using System.Diagnostics;

namespace AlperAslanApps.Core.Models
{
    [DebuggerStepThrough]
    public class EditInfo
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
    }
}
