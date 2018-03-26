using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System;

namespace RowinPt.Business.CommandHandlers.PersonalTrainers
{
    internal sealed class CreatePersonalTrainerCommandHandler : ICommandHandler<CreateCommand<PersonalTrainer>>
    {
        private readonly IRepository<PersonalTrainerModel> _personalTrainerRepository;

        public CreatePersonalTrainerCommandHandler(
            IRepository<PersonalTrainerModel> personalTrainerRepository)
        {
            _personalTrainerRepository = personalTrainerRepository;
        }

        public void Handle(CreateCommand<PersonalTrainer> command)
        {
            var model = command.Model;
            var user = new PersonalTrainerModel
            {
                Id = model.Id,
                Name = model.Name,
                Admin = model.IsAdmin,
                Sex = model.Sex,

                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            user.SecurityStamp = Guid.NewGuid();
            user.NormalizedEmail = model.Email.Normalize().ToUpperInvariant();
            user.EmailConfirmed = false;
            user.PasswordHash = null;

            _personalTrainerRepository.Add(user);
        }
    }
}
