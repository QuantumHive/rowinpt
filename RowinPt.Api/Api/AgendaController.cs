using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Contract.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries;
using RowinPt.Contract.Queries.Agenda;
using System;
using System.Collections.Generic;

namespace RowinPt.Api.Api
{
    [Authorize]
    [Route("agenda")]
    public class AgendaController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IUserContext _userContext;
        private readonly ICommandHandler<DeleteCommand<CustomerAgenda>> _deleteHandler;

        public AgendaController(
            IQueryProcessor queryProcessor,
            IUserContext userContext,
            ICommandHandler<DeleteCommand<CustomerAgenda>> deleteHandler)
        {
            _queryProcessor = queryProcessor;
            _userContext = userContext;
            _deleteHandler = deleteHandler;
        }

        [HttpGet("load/{locationId}")]
        public IEnumerable<Agenda> GetAgenda(Guid locationId, DateTime date)
        {
            var query = new GetAgendaQuery(locationId, date);
            return _queryProcessor.Process(query);
        }

        [HttpGet("{agendaId}")]
        public Agenda GetAgenda(Guid agendaId)
        {
            var query = new GetByIdQuery<Agenda>(agendaId);
            return _queryProcessor.Process(query);
        }

        [HttpGet("customer")]
        public IEnumerable<CustomerAgenda> GetCustomerAgenda()
        {
            var customerId = _userContext.GetId();
            var query = new GetCustomerAgendaQuery(customerId);
            return _queryProcessor.Process(query);
        }

        [HttpGet("customer/{agendaId}")]
        public CustomerAgenda GetCustomerAgenda(Guid agendaId)
        {
            var query = new GetByIdQuery<CustomerAgenda>(agendaId);
            return _queryProcessor.Process(query);
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            var command = new DeleteCommand<CustomerAgenda>(id);
            _deleteHandler.Handle(command);
            return NoContent();
        }
    }
}
