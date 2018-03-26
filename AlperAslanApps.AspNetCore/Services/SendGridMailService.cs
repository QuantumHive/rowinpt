using AlperAslanApps.AspNetCore.Models;
using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AlperAslanApps.AspNetCore.Services
{
    public class SendGridMailService : IEmailService
    {
        private readonly SendGridMailOptions _options;

        public SendGridMailService(
            IOptions<SendGridMailOptions> options)
        {
            _options = options.Value;
        }

        public void Send(EmailMessage message)
        {
            var client = new SendGridClient(_options.ApiKey);

            var from = new EmailAddress(message.FromAddress ?? _options.DefaultFromAddress);
            var to = new EmailAddress(message.ToAddress);

            var email = MailHelper.CreateSingleEmail(from, to, message.Subject, message.PlainTextContent, message.HtmlContent);

            var sendMailTask = client.SendEmailAsync(email);

            var response = sendMailTask.GetAwaiter().GetResult();
        }
    }
}
