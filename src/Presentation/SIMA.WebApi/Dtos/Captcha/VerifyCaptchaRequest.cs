namespace SIMA.WebApi.Dtos.Captcha;

public class VerifyCaptchaRequest
{
    public string UserInputCode { get; set; }
    public string Id { get; set; }
}