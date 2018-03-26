using System.Diagnostics;

namespace AlperAslanApps.Core.Models
{
    [DebuggerStepThrough]
    public class ValidationObject
    {
        public object Value { get; set; }
        public string Message { get; set; }
    }
}
