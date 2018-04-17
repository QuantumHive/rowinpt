using AlperAslanApps.Core;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Users;

namespace RowinPt.Api.Api
{
    [Route("information")]
    public class InformationController : Controller
    {
        private readonly ApplicationSettings _settings;
        private readonly IUserContext _userContext;
        private readonly IQueryProcessor _queryProcessor;

        public InformationController(
            ApplicationSettings settings,
            IUserContext userContext,
            IQueryProcessor queryProcessor)
        {
            _settings = settings;
            _userContext = userContext;
            _queryProcessor = queryProcessor;
        }

        [HttpGet]
        public Information Information()
        {
            var information = new Information
            {
                AppUri = _settings.AppUri,
                ManagementAppUri = _settings.ManagementAppUri,
                ApplicationTitle = _settings.ApplicationTitle,
                BlobStorageAccount = _settings.BlobStorageAccount,
            };

            return information;
        }

        [HttpGet("user")]
        public UserInformation UserInformation()
        {
            var userId = _userContext.GetId();
            var query = new GetUserInformationQuery(userId);
            return _queryProcessor.Process(query);
        }

        [HttpGet("version")]
        public string GetVersion() => _settings.Version;
    }

    public class Information
    {
        public string AppUri { get; set; }
        public string ManagementAppUri { get; set; }
        public string BlobStorageAccount { get; set; }
        public string ApplicationTitle { get; set; }
    }
}
