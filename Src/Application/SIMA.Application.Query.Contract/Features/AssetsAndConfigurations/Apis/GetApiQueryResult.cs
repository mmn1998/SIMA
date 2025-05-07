namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;

public class GetApiQueryResult
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
    public string? ApiTypeName { get; set; }
    public long? NetworkProtocolId { get; set; }
    public string? NetworkProtocolName { get; set; }
    public long? ApiMethodActionId { get; set; }
    public string? ApiMethodActionName { get; set; }
    public long? ApiAuthenticationMethodId { get; set; }
    public string? ApiAuthenticationMethodName { get; set; }
    public string? AuthenticationWorkflow { get; set; }
    public long? OwnerDepartmentId { get; set; }
    public string? OwnerDepartmentName { get; set; }
    public long? OwnerResponsibleId { get; set; }
    public string? OwnerResponsibleName { get; set; }
    public string? RulesAndConditions { get; set; }
    public string? ActiveStatus { get; set; }

    public List<ApiDocumentQueryResult>? ApiDocumentList { get; set; }
    public List<ApiSupportTeamQueryResult>? ApiSupportTeamList { get; set; }
    public List<ConfigurationItemApiQueryResult>? ConfigurationItemApiList { get; set; }
    public List<ApiRequestHeaderParamQueryResult>? ApiRequestHeaderParamList { get; set; }
    public List<ApiRequestBodyParamQueryResult>? ApiRequestBodyParamList { get; set; }
    public List<ApiResponseHeaderParamQueryResult>? ApiResponseHeaderParamList { get; set; }
    public List<ApiResponseBodyParamQueryResult>? ApiResponseBodyParamList { get; set; }
    public List<ApiRequestUrlParamQueryResult>? ApiRequestUrlParamList { get; set; }
    public List<ApiRequestQueryStringParamQueryResult>? ApiRequestQueryStringParamList { get; set; }
}
