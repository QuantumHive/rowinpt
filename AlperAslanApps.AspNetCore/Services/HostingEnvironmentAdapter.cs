using AlperAslanApps.Core;
using Microsoft.AspNetCore.Hosting;

namespace AlperAslanApps.AspNetCore.Services
{
    public class HostingEnvironmentAdapter : IEnvironment
    {
        public HostingEnvironmentAdapter(IHostingEnvironment hostingEnvironment)
        {
            IsDevelopment = hostingEnvironment.IsDevelopment();
        }

        public bool IsDevelopment { get; }
    }
}
