using System;
using System.Collections.Generic;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries;

namespace RowinPt.Api.Api
{
    [Route("scheduleitems")]
    public class ScheduleItemController : BaseCrudController<ScheduleItem>
    {
        public ScheduleItemController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<ScheduleItem>> createHandler,
            ICommandHandler<UpdateCommand<ScheduleItem>> editHandler,
            ICommandHandler<DeleteCommand<ScheduleItem>> deleteHandler)
            : base(
                queryProcessor,
                createHandler,
                editHandler,
                deleteHandler)
        {
        }

        [HttpGet("byschedule/{scheduleId}")]
        public IEnumerable<ScheduleItem> Read(Guid scheduleId)
        {
            var query = new GetAllScheduleItemsByScheduleIdQuery(scheduleId);
            return _queryProcessor.Process(query);
        }
    }
}
