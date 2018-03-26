using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace AlperAslanApps.Core.Exceptions
{
    [DebuggerStepThrough]
    public class AggregateValidationException : AggregateException
    {
        public AggregateValidationException(IEnumerable<ValidationException> innerExceptions)
            : base(innerExceptions)
        {
        }

        public new ValidationException InnerException => (ValidationException)base.InnerException;

        public new ReadOnlyCollection<ValidationException> InnerExceptions => new
            ReadOnlyCollection<ValidationException>(base.InnerExceptions.Cast<ValidationException>().ToArray());
    }
}
