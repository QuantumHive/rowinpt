using AlperAslanApps.Core.Contract.Models;

namespace AlperAslanApps.Core
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
