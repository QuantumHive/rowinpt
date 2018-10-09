namespace RowinPt.App.ReactJs
{
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            var version = typeof(Program).Assembly.GetName().Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}";
        }

        public string ApiEndpoint { get; set; }
        public string Version { get; }
        public string ApiVersion { get; set; }
        public string ApplicationTitle { get; set; }
    }

    public static class StaticSettings
    {
        public static string ApplicationTitle { get; set; }
        public static string BlobStorageAccount { get; set; }
    }
}
