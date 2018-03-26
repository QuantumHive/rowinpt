using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;

namespace RowinPt.Api.Api
{
    [Route("courses")]
    public class CourseController : BaseCrudController<Course>
    {
        public CourseController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<Course>> createHandler,
            ICommandHandler<UpdateCommand<Course>> editHandler,
            ICommandHandler<DeleteCommand<Course>> deleteHandler)
            : base(
                queryProcessor,
                createHandler,
                editHandler,
                deleteHandler)
        {
        }
    }
}
