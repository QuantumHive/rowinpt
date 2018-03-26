using System;
using System.Collections.Generic;
using System.Linq;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Models;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.Validators.PersonalTrainers
{
    internal sealed class EmailMustByUnique : IValidator<CreateCommand<PersonalTrainer>>, IValidator<UpdateCommand<PersonalTrainer>>
    {
        private readonly IReader<UserModel> _userReader;

        public EmailMustByUnique(IReader<UserModel> userReader)
        {
            _userReader = userReader;
        }

        public IEnumerable<ValidationObject> Validate(CreateCommand<PersonalTrainer> instance)
        {
            var user = _userReader.Entities.SingleOrDefault(pt =>
                pt.NormalizedEmail.Equals(instance.Model.Email.Normalize().ToUpperInvariant(), StringComparison.InvariantCultureIgnoreCase));

            if (user != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Er bestaat al een gebruiker met de email '{instance.Model.Email}'"
                };
            }
        }

        public IEnumerable<ValidationObject> Validate(UpdateCommand<PersonalTrainer> instance)
        {
            var user = _userReader.Entities.SingleOrDefault(pt =>
                pt.Id != instance.Model.Id &&
                pt.NormalizedEmail.Equals(instance.Model.Email.Normalize().ToUpperInvariant(), StringComparison.InvariantCultureIgnoreCase));

            if(user != null)
            {
                yield return new ValidationObject
                {
                    Message = $"Er bestaat al een gebruiker met de email '{instance.Model.Email}'"
                };
            }
        }
    }
}
