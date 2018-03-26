using AlperAslanApps.AspNetCore;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Models;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System.Linq;
using System.Net;

namespace RowinPt.Business.CommandHandlers.Account
{
    internal sealed class RequestPasswordResetCommandHandler : ICommandHandler<RequestPasswordResetCommand>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ICompanyContext _companyContext;
        private readonly IEmailService _emailService;
        private readonly IHost _host;

        public RequestPasswordResetCommandHandler(
            IReader<UserModel> userReader,
            ITokenGenerator tokenGenerator,
            ICompanyContext companyContext,
            IEmailService emailService,
            IHost host)
        {
            _userReader = userReader;
            _tokenGenerator = tokenGenerator;
            _companyContext = companyContext;
            _emailService = emailService;
            _host = host;
        }

        public void Handle(RequestPasswordResetCommand command)
        {
            var user = _userReader.Entities.SingleOrDefault(u => u.NormalizedEmail == command.NormalizedEmail);

            if(user != null)
            {
                if(user.EmailConfirmed)
                {
                    var token = _tokenGenerator.GenerateToken(user, "ResetPassword");
                    var urlEncodedToken = WebUtility.UrlEncode(token);
                    var resetUri = $"{_host.Uri}/account/reset?id={user.Id}&token={urlEncodedToken}";

                    SendMail(user, resetUri);
                }
            }
        }

        private void SendMail(UserModel user, string resetUri)
        {
            var message = new EmailMessage
            {
                ToAddress = user.Email,
                Subject = "Wachtwoord resetten",
                PlainTextContent = _companyContext.ResetPasswordTemplatePlainText(user.Name, resetUri),
                HtmlContent = _companyContext.ResetPasswordTemplateHtml(user.Name, resetUri)
            };

            _emailService.Send(message);
        }
    }
}
