using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace AlperAslanApps.AspNetCore.Services
{
    public class WebHost : IHost
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public WebHost(
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        public string Uri
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var builder = new StringBuilder();

                var scheme = context.Request.IsHttps ? "https://" : "http://";
                builder.Append(scheme);
                var host = context.Request.Host.Host;
                builder.Append(host);

                if (_hostingEnvironment.IsDevelopment())
                {
                    var port = context.Request.Host.Port;
                    builder.Append($":{port}");
                }

                return builder.ToString();
            }
        }
    }
}
