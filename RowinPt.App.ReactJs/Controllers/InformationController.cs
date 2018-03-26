using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace RowinPt.App.ReactJs.Controllers
{
    [Route("information")]
    public class InformationController : Controller
    {
        private readonly IOptions<ApplicationSettings> _settings;

        public InformationController(IOptions<ApplicationSettings> settings)
        {
            _settings = settings;
        }

        public ApplicationSettings Information()
        {
            var settings = _settings.Value;

            var client = new HttpClient();
            var call = client.GetAsync(settings.ApiEndpoint + "/information/version");
            var response = call.GetAwaiter().GetResult();
            var version = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            settings.ApiVersion = version;

            return settings;
        }
    }
}
