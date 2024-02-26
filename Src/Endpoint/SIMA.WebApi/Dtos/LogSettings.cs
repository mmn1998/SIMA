namespace SIMA.WebApi.Dtos
{
    public class LogSettings
    {
        public string ElasticsearchUri { get; set; }
        public string InformationFilePath { get; set; }
        public string ErrorFilePath { get; set; }
    }
}
