using SIMA.Application.Services.Request;
using SIMA.Application.Services.Response;

namespace SIMA.Application.Services
{
    public interface ISMSService
    {
        Task<TokenResponse> GetToken();
        Task<SendSMSResponse> SendSMS(SendSMSRequest request);
        Task<SendSMSResponse> SendSMSBulk(SendSMSBulkRequest request);


    }
}
