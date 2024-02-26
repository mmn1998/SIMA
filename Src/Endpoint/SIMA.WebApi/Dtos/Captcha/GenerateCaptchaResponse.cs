namespace SIMA.WebApi.Dtos.Captcha;

public class GenerateCaptchaResponse
{
    public byte[] Image { get; set; }
    public string Id { get; set; }
}
