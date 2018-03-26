using AlperAslanApps.Core;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Profile;
using System.Collections.Generic;

namespace RowinPt.Api.Api
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;

        public ProfileController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        [HttpGet]
        public IEnumerable<Measurement> Profile()
        {
            return _queryProcessor.Process(new GetCustomerProfileQuery());
        }
    }
}
