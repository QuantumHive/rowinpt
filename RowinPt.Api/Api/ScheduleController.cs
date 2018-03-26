using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;

namespace RowinPt.Api.Api
{
    [Route("schedule")]
    public class ScheduleController : BaseCrudController<Schedule>
    {
        public ScheduleController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<Schedule>> createHandler,
            ICommandHandler<UpdateCommand<Schedule>> editHandler,
            ICommandHandler<DeleteCommand<Schedule>> deleteHandler)
            : base(
                queryProcessor,
                createHandler,
                editHandler,
                deleteHandler)
        {
        }
    }
}
