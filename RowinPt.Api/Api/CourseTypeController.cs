using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;

namespace RowinPt.Api.Api
{
    [Route("coursetypes")]
    public class CourseTypeController : BaseCrudController<CourseType>
    {
        public CourseTypeController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<CourseType>> createHandler,
            ICommandHandler<UpdateCommand<CourseType>> editHandler,
            ICommandHandler<DeleteCommand<CourseType>> deleteHandler)
            : base(
                queryProcessor,
                createHandler,
                editHandler,
                deleteHandler)
        {
        }
    }
}
