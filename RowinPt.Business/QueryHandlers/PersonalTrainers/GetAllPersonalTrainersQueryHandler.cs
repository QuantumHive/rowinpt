using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RowinPt.Business.QueryHandlers.PersonalTrainers
{
    internal sealed class GetAllPersonalTrainersQueryHandler : IQueryHandler<GetAllQuery<PersonalTrainer>, IEnumerable<PersonalTrainer>>
    {
        private readonly IReader<PersonalTrainerModel> _personalTrainerReader;

        public GetAllPersonalTrainersQueryHandler(IReader<PersonalTrainerModel> personalTrainerReader)
        {
            _personalTrainerReader = personalTrainerReader;
        }

        public IEnumerable<PersonalTrainer> Handle(GetAllQuery<PersonalTrainer> query)
        {
            return _personalTrainerReader.Entities.Select(trainer => new PersonalTrainer
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Sex = trainer.Sex,
                Email = trainer.Email,
                PhoneNumber = trainer.PhoneNumber,
                IsAdmin = trainer.Admin,
                EmailConfirmed = trainer.EmailConfirmed,
            });
        }
    }
}
