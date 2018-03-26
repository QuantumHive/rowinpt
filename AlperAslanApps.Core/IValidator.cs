using AlperAslanApps.Core.Models;
using System.Collections.Generic;

namespace AlperAslanApps.Core
{
    public interface IValidator<T>
    {
        IEnumerable<ValidationObject> Validate(T instance);
    }
}
