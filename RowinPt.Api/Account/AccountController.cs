using AlperAslanApps.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Commands.Account;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Users;

namespace RowinPt.Api.Account
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly ICommandHandler<ActivateAccountCommand> _activationHandler;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IUserContext _userContext;

        public AccountController(
            ISessionManager sessionManager,
            ICommandHandler<ActivateAccountCommand> activationHandler,
            IHostingEnvironment hostingEnvironment,
            IQueryProcessor queryProcessor,
            ApplicationSettings applicationSettings,
            IUserContext userContext)
        {
            _sessionManager = sessionManager;
            _activationHandler = activationHandler;
            _hostingEnvironment = hostingEnvironment;
            _queryProcessor = queryProcessor;
            _applicationSettings = applicationSettings;
            _userContext = userContext;
        }

        [HttpGet("challenge")]
        public IActionResult IsAuthenticated()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _userContext.GetId();
                var isCustomer = _queryProcessor.Process(new IsUserCustomerQuery(userId));
                return Accepted(isCustomer ? _applicationSettings.AppUri : _applicationSettings.ManagementAppUri);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentials credentials)
        {
            _sessionManager.SignIn(credentials);

            var userId = _userContext.GetId();
            var isCustomer = _queryProcessor.Process(new IsUserCustomerQuery(userId));
            return Accepted(isCustomer ? _applicationSettings.AppUri : _applicationSettings.ManagementAppUri);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _sessionManager.SignOut();
            return NoContent();
        }

        [HttpPost("activate")]
        public IActionResult Activate([FromBody] Activation activation)
        {
            var command = new ActivateAccountCommand(activation);
            _activationHandler.Handle(command);

            return NoContent();
        }
    }
}
