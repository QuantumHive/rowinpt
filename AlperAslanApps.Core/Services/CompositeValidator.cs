using AlperAslanApps.Core.Models;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlperAslanApps.Core.Services
{
    [DebuggerStepThrough]
    public class CompositeValidator<T> : IValidator<T>
        where T : class
    {
        private readonly IEnumerable<IValidator<T>> _validators;

        public CompositeValidator(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public IEnumerable<ValidationObject> Validate(T instance)
        {
            instance.ThrowIfNull(nameof(instance));

            var validationResults = GetValidationResults(instance);

            foreach (var result in validationResults)
            {
                result.Value = instance;
                yield return result;
            }
        }

        private IEnumerable<ValidationObject> GetValidationResults(T instance) =>
            _validators.SelectMany(v => v.Validate(instance));
    }
}
