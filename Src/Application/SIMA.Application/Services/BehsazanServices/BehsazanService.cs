using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SIMA.Application.Services.BehsazanServices.Request;
using SIMA.Application.Services.BehsazanServices.Response;
using SIMA.Application.Services.Request;
using SIMA.Application.Services.Response;
using SIMA.Framework.Infrastructure.RestfulClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace SIMA.Application.Services.BehsazanServices
{
    public class BehsazanService : IBehsazanService
    {
        private readonly IRestfulClient _restfulClient;
        private readonly BehsazanServiceSetting _behsazanServiceSetting;
        private readonly string BaseUrl;

        public BehsazanService(IRestfulClient restfulClient, IOptions<BehsazanServiceSetting> smsSetting)
        {
            _restfulClient = restfulClient;
            _behsazanServiceSetting = smsSetting.Value;
            BaseUrl = _behsazanServiceSetting.BaseUrl;
        }
        public async Task<GetTrustCurrencyDraftMansha> GetTrustyDraft(TrustCurrencyDraft input)
        {
            try
            {
                var _httpClient = new HttpClient();

                var username = _behsazanServiceSetting.Username;

                var password = _behsazanServiceSetting.Password;

                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var url = new Uri(new Uri(BaseUrl), _behsazanServiceSetting.GetTrustCurrencyDraftManshaURL).ToString();
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                var jsonContent = JsonConvert.SerializeObject(input);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();


                //var responseContent = "{\r\n    \"timestamp\": \"2024-12-09T14:57:33.386+0330\",\r\n    \"status\": 200,\r\n    \"error\": \"\",\r\n    \"message\": \"عملیات موفقیت آمیز\",\r\n    \"path\": \"/v1/trustcurrencydraft/export\",\r\n    \"exp\": \"INT\",\r\n    \"data\": [\r\n        {\r\n            \"orderingName\": \"nasajekhoye\",\r\n            \"orderingExtAccNo\": \"4659427193\",\r\n            \"orderId\": 24001052124,\r\n            \"draftId\": \"635291403800013\",\r\n            \"instructedAmnt\": 36008.80000,\r\n            \"instructedArzCd\": 11,\r\n            \"settlementAmnt\": 36008.80000,\r\n            \"bSettlementAmnt\": 36008.80000,\r\n            \"bInstructedAmnt\": 36008.80000,\r\n            \"regDate\": 14030115,\r\n            \"settlementArzCd\": 11,\r\n            \"draftFrom\": 1,\r\n            \"draftFromMsg\": \"ثبت سفارش\",\r\n            \"extCustCd\": 232800732170,\r\n            \"blockAdeNo\": 0,\r\n            \"agentShareSts\": 0,\r\n            \"agentShareStsMsg\": \"غیرعامل\",\r\n            \"crspndntAccTyp\": 1,\r\n            \"crspndntAccTypMsg\": \"نوسترو\",\r\n            \"chrgAmnt\": 21790747.00000,\r\n            \"chrgTyp\": 2,\r\n            \"chrgTypMsg\": \"BEN\",\r\n            \"rvwrslt\": 1,\r\n            \"rvwrsltMsg\": \"ثبت\",\r\n            \"regBranchCd\": 63529,\r\n            \"titlex\": \"مستقل مرکزي\",\r\n            \"valueDate\": 20240403,\r\n            \"crspndntBank\": \"TSAK\",\r\n            \"benExtAccNo\": \"\",\r\n            \"draftSts\": 6,\r\n            \"draftStsMsg\": \"تولید سوئیفت\",\r\n            \"draftDestTyp\": 1,\r\n            \"draftDestTypMsg\": \"به بانک های خارج از کشور\",\r\n            \"payerBank\": \"TSAK\",\r\n            \"benIban\": \"1\",\r\n            \"benName\": \"acn kimyasal\",\r\n            \"benTel\": \"\",\r\n            \"benAddress\": \"turkey\",\r\n            \"crspndntBic\": \"VHDTSACOXXX\",\r\n            \"payerBankBic\": \"VHDTSACOXXX\",\r\n            \"interMedBankBic\": \"\",\r\n            \"interMedBankName\": \"\",\r\n            \"remittanceInfoCd\": 8,\r\n            \"draftReasonDesc\": \"ISSUE FROM CB\",\r\n            \"remittanceInfoDesc\": \"PAYMENT FOR P/I NO.  10227  CB NO.  28209111\",\r\n            \"confirmDate\": 14030115,\r\n            \"confirmTime\": 121730,\r\n            \"orderingAddress\": \"TEHRAN\",\r\n            \"orderingTel\": \"42179000\",\r\n            \"nationalCode\": 10101181267,\r\n            \"benPassport\": \"0\",\r\n            \"revokeReason\": 0,\r\n            \"revokeReasonMsg\": null,\r\n            \"reqValueTyp\": 2,\r\n            \"reqValueTypMsg\": \"Same Day\",\r\n            \"blockedAmnt\": 0.00000,\r\n            \"facilityType\": 1,\r\n            \"facilityTypeMsg\": \"تسهیلات\",\r\n            \"revokeDraftRefNo\": null,\r\n            \"revokeArzAmnt\": null,\r\n            \"revokeArzCd\": 11,\r\n            \"revokeDt\": 0,\r\n            \"revokeValueDt\": 0,\r\n            \"disapprovalReasonCd\": 0,\r\n            \"disapprovalReasonDesc\": null,\r\n            \"payTyp\": 1,\r\n            \"rvwRmk\": null,\r\n            \"draftRmk\": \"INFORMATION NERKH MOBADELE, DD: 14030114\",\r\n            \"draftNo\": 28209111\r\n        }\r\n    ]\r\n}";


                var result = JsonConvert.DeserializeObject<GetTrustCurrencyDraftMansha>(responseContent);
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا: {ex.Message}");
                throw;
            }

        }
    }
}

