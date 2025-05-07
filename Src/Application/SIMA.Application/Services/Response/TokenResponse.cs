namespace SIMA.Application.Services.Response
{
    public class TokenResponse
    {
        public string Scope { get; set; }
        public string Access_token { get; set; }
        public DateTime Expires_at { get; set; }
    }
}
