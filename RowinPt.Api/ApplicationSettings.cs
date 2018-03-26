using Microsoft.Extensions.Configuration;

namespace RowinPt.Api
{
    public class ApplicationSettings
    {
        public ApplicationSettings(IConfiguration configuration)
        {
            AppUri = configuration[ConfigurationKeys.AppUri];
            ManagementAppUri = configuration[ConfigurationKeys.ManagementAppUri];
        }

        public string AppUri { get; }
        public string ManagementAppUri { get; }
        public string Version
        {
            get
            {
                var version = typeof(Program).Assembly.GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
    }
}
