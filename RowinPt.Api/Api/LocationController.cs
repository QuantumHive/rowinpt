using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Commands;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;

namespace RowinPt.Api.Api
{
    [Route("locations")]
    public class LocationController : BaseCrudController<Location>
    {
        public LocationController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateCommand<Location>> createHandler,
            ICommandHandler<UpdateCommand<Location>> editHandler,
            ICommandHandler<DeleteCommand<Location>> deleteHandler)
            : base(
                queryProcessor,
                createHandler,
                editHandler,
                deleteHandler)
        {
        }
    }
}
