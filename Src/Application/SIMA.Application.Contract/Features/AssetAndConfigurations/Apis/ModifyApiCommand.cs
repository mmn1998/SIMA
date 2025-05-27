using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Apis;

public class ModifyApiCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public string? IsInternalApi { get; set; }
    public string? Prerequisites { get; set; }
    public string? BaseUrl { get; set; }
    public string? ApiAddress { get; set; }
    public string? IpAddress { get; set; }
    public string? PortNumber { get; set; }
    public int? RateLimitingMin { get; set; }
    public int? RateLimitingMax { get; set; }
    public long? ApiTypeId { get; set; }
    public long? ApiAuthenticationMethodId { get; set; }
    public long? NetworkProtocolId { get; set; }
    public string? AuthenticationWorkflow { get; set; }
    public long? OwnerDepartmentId { get; set; }
    public long? OwnerResponsibleId { get; set; }
    public string? RulesAndConditions { get; set; }

    public List<CreateApiDocument> ApiDocumentList { get; set; }
    public List<CreateApiSupportTeam> ApiSupportTeamList { get; set; }
    public List<CreateConfigurationItemApi> ConfigurationItemApiList { get; set; }
    public List<CreateApiRequestHeaderParam> ApiRequestHeaderParamList { get; set; }
    public List<CreateApiRequestBodyParam> ApiRequestBodyParamList { get; set; }
    public List<CreateApiResponseHeaderParam> ApiResponseHeaderParamList { get; set; }
    public List<CreateApiResponseBodyParam> ApiResponseBodyParamList { get; set; }
    public List<CreateApiRequestUrlParam> ApiRequestUrlParamList { get; set; }
    public List<CreateApiRequestQueryStringParam> ApiRequestQueryStringParamList { get; set; }
}