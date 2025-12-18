namespace SIMA.WebApi.Settings
{
    public class ApplicationSettings
    {
        public LicenseSettings LicenseSettings { get; set; }
    }
    public class LicenseSettings
    {
        public string LicensePublicKey { get; set; }
        public string LicensePath { get; set; }
    }
}
