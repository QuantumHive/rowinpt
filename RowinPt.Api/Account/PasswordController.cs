using AlperAslanApps.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Commands.Account;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Users;

namespace RowinPt.Api.Account
{
    [Route("account/password")]
    public class PasswordController : Controller
    {
        private readonly ICommandHandler<ChangePasswordCommand> _changePasswordHandler;
        private readonly ICommandHandler<RequestPasswordResetCommand> _requestPasswordResetHandler;
        private readonly ICommandHandler<ResetPasswordCommand> _resetHandler;
        private readonly ISessionManager _sessionManager;
        private readonly IUserContext _userContext;
        private readonly IQueryProcessor _queryProcessor;

        public PasswordController(
            ICommandHandler<ChangePasswordCommand> changePasswordHandler,
            ICommandHandler<RequestPasswordResetCommand> requestPasswordResetHandler,
            ICommandHandler<ResetPasswordCommand> resetHandler,
            ISessionManager sessionManager,
            IUserContext userContext,
            IQueryProcessor queryProcessor)
        {
            _changePasswordHandler = changePasswordHandler;
            _requestPasswordResetHandler = requestPasswordResetHandler;
            _resetHandler = resetHandler;
            _sessionManager = sessionManager;
            _userContext = userContext;
            _queryProcessor = queryProcessor;
        }

        [Authorize]
        [HttpPut("change")]
        public IActionResult Change([FromBody] ChangePasswordCommand command)
        {
            _changePasswordHandler.Handle(command);

            _sessionManager.SignOut();
            SignIn(command.NewPassword);

            return NoContent();
        }

        private void SignIn(string password)
        {
            var userId = _userContext.GetId();
            var userInformation = _queryProcessor.Process(new GetUserInformationQuery(userId));
            var credentials = new LoginCredentials(userInformation.Email, password);
            _sessionManager.SignIn(credentials);
        }

        [HttpPost("forgot")]
        public IActionResult Forgot([FromBody] LoginCredentials credentials)
        {
            _requestPasswordResetHandler.Handle(new RequestPasswordResetCommand(credentials.Email));
            return NoContent();
        }

        [HttpPost("reset")]
        public IActionResult Reset([FromBody] ResetPasswordCommand command)
        {
            _resetHandler.Handle(command);
            return NoContent();
        }
    }
}
