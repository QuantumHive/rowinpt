using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using AlperAslanApps.Core.Contract.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace RowinPt.Api.Api
{
    [Authorize]
    public abstract class BaseCrudController<TModel> : Controller
        where TModel : class, IIdentifier
    {
        protected readonly IQueryProcessor _queryProcessor;
        protected readonly ICommandHandler<CreateCommand<TModel>> _createHandler;
        protected readonly ICommandHandler<UpdateCommand<TModel>> _editHandler;
        protected readonly ICommandHandler<DeleteCommand<TModel>> _deleteHandler;

        public BaseCrudController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<TModel>> createHandler,
            ICommandHandler<UpdateCommand<TModel>> editHandler,
            ICommandHandler<DeleteCommand<TModel>> deleteHandler)
        {
            _queryProcessor = queryProcessor;
            _createHandler = createHandler;
            _editHandler = editHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpGet]
        public virtual IEnumerable<TModel> Read()
        {
            var query = new GetAllQuery<TModel>();
            return _queryProcessor.Process(query);
        }

        [HttpGet("{id}")]
        public virtual TModel Get(Guid id)
        {
            var query = new GetByIdQuery<TModel>(id);
            return _queryProcessor.Process(query);
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] TModel model)
        {
            var id = Guid.NewGuid();
            model.Id = id;

            var command = new CreateCommand<TModel>(model);
            _createHandler.Handle(command);

            return NoContent();
        }

        [HttpPut]
        public virtual IActionResult Update([FromBody] TModel location)
        {
            var command = new UpdateCommand<TModel>(location);
            _editHandler.Handle(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            var command = new DeleteCommand<TModel>(id);
            _deleteHandler.Handle(command);
            return NoContent();
        }
    }
}
