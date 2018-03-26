using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Queries;
using RowinPt.Contract.Models;
using RowinPt.Domain;

namespace RowinPt.Business.QueryHandlers.PersonalTrainers
{
    internal sealed class GetPersonalTrainerByIdQueryHandler : IQueryHandler<GetByIdQuery<PersonalTrainer>, PersonalTrainer>
    {
        private readonly IReader<PersonalTrainerModel> _personalTrainerReader;

        public GetPersonalTrainerByIdQueryHandler(IReader<PersonalTrainerModel> personalTrainerReader)
        {
            _personalTrainerReader = personalTrainerReader;
        }

        public PersonalTrainer Handle(GetByIdQuery<PersonalTrainer> query)
        {
            var trainer = _personalTrainerReader.GetById(query.Id);

            return new PersonalTrainer
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Sex = trainer.Sex,
                Email = trainer.Email,
                PhoneNumber = trainer.PhoneNumber,
                IsAdmin = trainer.Admin,
                EmailConfirmed = trainer.EmailConfirmed,
            };
        }
    }
}
