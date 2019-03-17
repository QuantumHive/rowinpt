using System.Collections.Generic;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Commands.Customers;
using RowinPt.Domain;

namespace RowinPt.Business.Validators.Customers
{
    internal sealed class CannotSubmitMeasurementsForNonConfirmedCustomers : IValidator<SubmitMeasurementForCustomerCommand>
    {
        private readonly IReader<UserModel> _userReader;

        public CannotSubmitMeasurementsForNonConfirmedCustomers(IReader<UserModel> userReader)
        {
            _userReader = userReader;
        }

        public IEnumerable<ValidationObject> Validate(SubmitMeasurementForCustomerCommand instance)
        {
            var user = _userReader.GetById(instance.CustomerId);

            if(!user.EmailConfirmed)
            {
                yield return new ValidationObject
                {
                    Message = $"Account van gebruiker met email '{user.Email}' is niet bevestigd: kan geen meetgegevens toevoegen.",
                };
            }
        }
    }
}
