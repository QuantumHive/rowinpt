using Microsoft.Extensions.Configuration;

namespace RowinPt.Api
{
    public class ApplicationSettings
    {
        static ApplicationSettings()
        {
            var version = typeof(Program).Assembly.GetName().Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}";
        }

        public ApplicationSettings(IConfiguration configuration)
        {
            AppUri = configuration[ConfigurationKeys.AppUri];
            ManagementAppUri = configuration[ConfigurationKeys.ManagementAppUri];
            ApplicationTitle = configuration[ConfigurationKeys.ApplicationTitle];
            BlobStorageAccount = configuration[ConfigurationKeys.BlobStorageAccount];

            
        }

        public string AppUri { get; }
        public string ManagementAppUri { get; }
        public string BlobStorageAccount { get; }
        public string ApplicationTitle { get; }
        public static string Version { get; }
    }

    public static class StaticSettings
    {
        public static string ApplicationTitle { get; set; }
        public static string BlobStorageAccount { get; set; }
    }
}
