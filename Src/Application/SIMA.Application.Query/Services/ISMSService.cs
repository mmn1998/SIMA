using Azure.Core;
using SIMA.Application.Query.Services.Request;
using SIMA.Application.Query.Services.Response;

namespace SIMA.Application.Query.Services
{
    public interface ISMSService
    {
        Task<TokenResponse> GetToken();
        Task<SendSMSResponse> SendSMS(SendSMSRequest request);
        Task<SendSMSResponse> SendSMSBulk(SendSMSBulkRequest request);


    }
}
