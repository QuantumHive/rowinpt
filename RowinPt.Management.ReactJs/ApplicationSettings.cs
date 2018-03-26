namespace RowinPt.Management.ReactJs
{
    public class ApplicationSettings
    {
        public string ApiEndpoint { get; set; }
        public string Version
        {
            get
            {
                var version = typeof(Program).Assembly.GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
        public string ApiVersion { get; set; }
    }
}
