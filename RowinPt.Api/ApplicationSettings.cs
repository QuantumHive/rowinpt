namespace RowinPt.Api
{
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            var version = typeof(Program).Assembly.GetName().Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}";
        }

        public string AppUri { get; set; }
        public string ManagementAppUri { get; set; }
        public string BlobStorageAccount { get; set; }
        public string ApplicationTitle { get; set; }
        public string Version { get; set; }
    }

    public static class StaticSettings
    {
        public static string ApplicationTitle { get; set; }
        public static string BlobStorageAccount { get; set; }
    }
}

