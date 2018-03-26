using System;

namespace AlperAslanApps.Core.Services
{
    public class CompanyContext : ICompanyContext
    {
        public CompanyContext(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}
