using AlperAslanApps.Core;
using AlperAslanApps.Core.Contract.Models;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;

namespace AlperAslanApps.AspNetCore.Services
{
    public class TelemetryMailService : IEmailService
    {
        private readonly TelemetryClient _telemetry;

        public TelemetryMailService(
            TelemetryClient telemetry)
        {
            _telemetry = telemetry;
        }

        public void Send(EmailMessage message)
        {
            var properties = new Dictionary<string, string>
            {
                {nameof(EmailMessage.FromAddress), message.FromAddress },
                {nameof(EmailMessage.ToAddress), message.ToAddress },
                {nameof(EmailMessage.Subject), message.Subject },
                {nameof(EmailMessage.PlainTextContent), message.PlainTextContent },
                {nameof(EmailMessage.HtmlContent), message.HtmlContent },
            };

            _telemetry.TrackEvent("Email sent", properties);
        }
    }
}
