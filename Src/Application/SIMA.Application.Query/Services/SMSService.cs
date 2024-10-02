using Flurl;
using Microsoft.Extensions.Options;
using SIMA.Application.Query.Services.Request;
using SIMA.Application.Query.Services.Response;
using SIMA.Framework.Infrastructure.RestfulClient;

namespace SIMA.Application.Query.Services
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
                { "scope", "sima" },
                { "username", _smsSetting.Username },
                { "password", _smsSetting.Password }
            };

            var response = await _restfulClient.PostURLEnCodedAsync<TokenResponse, TokenRequest>(BaseUrl + _smsSetting.SMSTokenURL, postData);
            return response;
        }

        public async Task<SendSMSResponse> SendSMS(SendSMSRequest request)
        {
            var token = await GetToken();
            var header = new Dictionary<string, string>();
            header.Add("Authorization", $"Bearer {token.Access_token}");

            var postData = new Dictionary<string, string>
            {
                { "SourceAddress", request.SourceAddress },
                { "DestinationAddress", request.DestinationAddress },
                { "MessageText", request.MessageText },
                { "ValidityPeriod", request.ValidityPeriod.ToString() }
            };

            var response = await _restfulClient.PostURLEnCodedAsync<SendSMSResponse, SendSMSRequest>(BaseUrl + _smsSetting.SendSMSURL, postData, header);
            return response;
        }

        public Task<SendSMSResponse> SendSMSBulk(SendSMSBulkRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
