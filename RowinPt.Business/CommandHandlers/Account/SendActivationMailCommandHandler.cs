using AlperAslanApps.AspNetCore;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Models;
using Microsoft.AspNetCore.DataProtection;
using RowinPt.Contract.Commands.Account;
using RowinPt.Domain;
using System;
using System.Net;

namespace RowinPt.Business.CommandHandlers.Account
{
    internal sealed class SendActivationMailCommandHandler : ICommandHandler<SendActivationMailCommand>
    {
        private readonly IReader<UserModel> _userReader;
        private readonly ITokenProvider<UserModel> _tokenProvider;
        private readonly IDataProtector _dataProtector;
        private readonly ICompanyContext _companyContext;
        private readonly IEmailService _emailService;
        private readonly IHost _host;

        public SendActivationMailCommandHandler(
            IReader<UserModel> userReader,
            ITokenProvider<UserModel> tokenProvider,
            IDataProtector dataProtector,
            ICompanyContext companyContext,
            IEmailService emailService,
            IHost host)
        {
            _userReader = userReader;
            _tokenProvider = tokenProvider;
            _dataProtector = dataProtector;
            _companyContext = companyContext;
            _emailService = emailService;
            _host = host;
        }

        public void Handle(SendActivationMailCommand command)
        {
            var user = _userReader.GetById(command.UserId);
            var token = GenerateToken(user);

            var urlEncodedToken = WebUtility.UrlEncode(token);
            var activationUri = $"{_host.Uri}/account/activate?id={user.Id}&token={urlEncodedToken}";

            SendMail(user, activationUri);
        }

        private string GenerateToken(UserModel user)
        {
            var unprotectedToken = _tokenProvider.Generate(user, "AccountActivation");
            var protectedToken = _dataProtector.Protect(unprotectedToken);
            var token = Convert.ToBase64String(protectedToken);
            return token;
        }

        private void SendMail(UserModel user, string activationUri)
        {
            var message = new EmailMessage
            {
                ToAddress = user.Email,
                Subject = "Persoonlijke account",
                PlainTextContent = _companyContext.AccountActivationTemplatePlainText(user.Name, activationUri),
                HtmlContent = _companyContext.AccountActivationTemplateHtml(user.Name, activationUri)
            };

            _emailService.Send(message);
        }
    }
}
