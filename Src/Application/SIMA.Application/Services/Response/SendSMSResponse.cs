namespace SIMA.Application.Services.Response
{
    public class SendSMSResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public string[] Data { get; set; }
        public int ResultCode { get; set; }
    }
}
