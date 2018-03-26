using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Commands.Account;
using RowinPt.Contract.Models;
using System;

namespace RowinPt.Api.Api
{
    [Route("personaltrainers")]
    public class PersonalTrainerController : BaseCrudController<PersonalTrainer>
    {
        private readonly ICommandHandler<SendActivationMailCommand> _sendActivationMailHandler;

        public PersonalTrainerController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<PersonalTrainer>> createHandler,
            ICommandHandler<UpdateCommand<PersonalTrainer>> editHandler,
            ICommandHandler<DeleteCommand<PersonalTrainer>> deleteHandler,
            ICommandHandler<SendActivationMailCommand> sendActivationMailHandler)
            : base(
                queryProcessor,
                createHandler,
                editHandler,
                deleteHandler)
        {
            _sendActivationMailHandler = sendActivationMailHandler;
        }

        [HttpPost]
        public override IActionResult Create([FromBody] PersonalTrainer model)
        {
            var id = Guid.NewGuid();
            model.Id = id;

            var command = new CreateCommand<PersonalTrainer>(model);
            _createHandler.Handle(command);

            _sendActivationMailHandler.Handle(new SendActivationMailCommand(id));

            return NoContent();
        }

        [HttpPost("resendactivation/{id}")]
        public IActionResult ResendActivation(Guid id)
        {
            var command = new SendActivationMailCommand(id);
            _sendActivationMailHandler.Handle(command);
            return NoContent();
        }
    }
}
