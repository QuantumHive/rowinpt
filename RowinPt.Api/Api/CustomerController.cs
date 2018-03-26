using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Contract.Queries;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Commands.Account;
using RowinPt.Contract.Commands.Customers;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Customers;
using System;
using System.Collections.Generic;

namespace RowinPt.Api.Api
{
    [Route("customers")]
    public class CustomerController : BaseCrudController<Customer>
    {
        private readonly ICommandHandler<SubmitMeasurementForCustomerCommand> _submitMeasurementHandler;
        private readonly ICommandHandler<SendActivationMailCommand> _sendActivationMailHandler;
        private readonly ICommandHandler<SaveAbsenceNotesForCustomerCommand> _saveNotesHandler;

        public CustomerController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<Customer>> createHandler,
            ICommandHandler<UpdateCommand<Customer>> editHandler,
            ICommandHandler<DeleteCommand<Customer>> deleteHandler,
            ICommandHandler<SubmitMeasurementForCustomerCommand> submitMeasurementHandler,
            ICommandHandler<SendActivationMailCommand> sendActivationMailHandler,
            ICommandHandler<SaveAbsenceNotesForCustomerCommand> saveNotesHandler)
            : base(
                queryProcessor,
                createHandler,
                editHandler,
                deleteHandler)
        {
            _submitMeasurementHandler = submitMeasurementHandler;
            _sendActivationMailHandler = sendActivationMailHandler;
            _saveNotesHandler = saveNotesHandler;
        }

        [HttpPost]
        public override IActionResult Create([FromBody] Customer model)
        {
            var id = Guid.NewGuid();
            model.Id = id;

            var command = new CreateCommand<Customer>(model);
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

        [HttpGet("measurements/{id}")]
        public IEnumerable<Measurement> Measurements(Guid id)
        {
            var query = new GetMeasurementsForCustomerQuery(id);
            return _queryProcessor.Process(query);
        }

        [HttpPut("measurements/{id}")]
        public IActionResult SubmitMeasurement([FromBody]Measurement measurement, Guid id)
        {
            var command = new SubmitMeasurementForCustomerCommand(id, measurement);
            _submitMeasurementHandler.Handle(command);
            return NoContent();
        }

        [HttpGet("absentees/{weeks}")]
        public IEnumerable<AbsentCustomer> GetAbsentees(int weeks)
        {
            var query = new GetAbsentCustomersByWeekQuery(weeks);
            return _queryProcessor.Process(query);
        }

        [HttpGet("absentees/details/{id}")]
        public AbsentCustomerDetails GetAbsentCustomer(Guid id)
        {
            var query = new GetByIdQuery<AbsentCustomerDetails>(id);
            return _queryProcessor.Process(query);
        }

        [HttpPut("absentees/save/notes")]
        public IActionResult SaveNotesForAbsentCustomer([FromBody]SaveAbsenceNotesForCustomerCommand command)
        {
            _saveNotesHandler.Handle(command);
            return NoContent();
        }
    }
}
