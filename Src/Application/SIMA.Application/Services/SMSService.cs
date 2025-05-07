using Flurl;
using Microsoft.Extensions.Options;
using SIMA.Application.Services.Request;
using SIMA.Application.Services.Response;
using SIMA.Framework.Infrastructure.RestfulClient;

namespace SIMA.Application.Services
{
    public class SMSService : ISMSService
    {
        private readonly IRestfulClient _restfulClient;
        private readonly SMSSetting _smsSetting;
        private readonly string BaseUrl;

        public SMSService(IRestfulClient restfulClient, IOptions<SMSSetting> smsSetting)
        {
            _restfulClient = restfulClient;
            _smsSetting = smsSetting.Value;
            BaseUrl = _smsSetting.BaseURL;
        }
        public async Task<TokenResponse> GetToken()
        {

            var postData = new Dictionary<string, string>
            {
                { "scope", "ApiAccess" },
                { "username", _smsSetting.Username },
                { "password", _smsSetting.Password }
            };

            var response = await _restfulClient.PostURLEnCodedAsync<TokenResponse, TokenRequest>(BaseUrl + _smsSetting.SMSTokenURL, postData);
            return response;
        }

        public async Task<SendSMSResponse> SendSMS(SendSMSRequest request)
        {
            var token = await GetToken();
            request.SourceAddress = _smsSetting.SourceAddress;
            var header = new Dictionary<string, string>();
            header.Add("Authorization", $"Bearer {token.Access_token}");

            var messages = new List<SendSMSRequest>();

            messages.Add(request);  

            var response = await _restfulClient.PostAsync<SendSMSResponse, List<SendSMSRequest>>(BaseUrl + _smsSetting.SendSMSURL, messages, header);
            return response;
        }

        public Task<SendSMSResponse> SendSMSBulk(SendSMSBulkRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
