using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.CommandHandlers.PersonalTrainers
{
    internal sealed class UpdatePersonalTrainerCommandHandler : ICommandHandler<UpdateCommand<PersonalTrainer>>
    {
        private readonly IReader<PersonalTrainerModel> _personalTrainerReader;

        public UpdatePersonalTrainerCommandHandler(
            IReader<PersonalTrainerModel> personalTrainerReader)
        {
            _personalTrainerReader = personalTrainerReader;
        }

        public void Handle(UpdateCommand<PersonalTrainer> command)
        {
            var trainer = _personalTrainerReader.GetById(command.Model.Id);

            trainer.Name = command.Model.Name;
            trainer.PhoneNumber = command.Model.PhoneNumber;
            trainer.Sex = command.Model.Sex;
            trainer.Admin = command.Model.IsAdmin;

            if (!trainer.EmailConfirmed)
            {
                trainer.Email = command.Model.Email;
            }
        }
    }
}
