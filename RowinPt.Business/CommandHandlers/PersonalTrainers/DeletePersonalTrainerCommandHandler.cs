using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Linq;

namespace RowinPt.Business.CommandHandlers.PersonalTrainers
{
    internal sealed class DeletePersonalTrainerCommandHandler : ICommandHandler<DeleteCommand<PersonalTrainer>>
    {
        private readonly IReader<PersonalTrainerModel> _personalTrainerReader;
        private readonly IReader<ScheduleItemModel> _scheduleItemReader;

        public DeletePersonalTrainerCommandHandler(
            IReader<PersonalTrainerModel> personalTrainerReader,
            IReader<ScheduleItemModel> scheduleItemReader)
        {
            _personalTrainerReader = personalTrainerReader;
            _scheduleItemReader = scheduleItemReader;
        }

        public void Handle(DeleteCommand<PersonalTrainer> command)
        {
            var scheduleItems = _scheduleItemReader.Entities.Where(item => item.PersonalTrainerId == command.Id);

            foreach(var scheduleItem in scheduleItems)
            {
                scheduleItem.PersonalTrainerId = null;
            }

            _personalTrainerReader.DeleteById(command.Id);
        }
    }
}
