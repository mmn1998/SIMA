using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.Entities;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class Api : Entity, IAggregateRoot
{
    private Api() { }
    private Api(CreateApiArg arg)
    {
        Id = new ApiId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        Description = arg.Description;
        Prerequisites = arg.Prerequisites;
        ApiAddress = arg.ApiAddress;
        PortNumber = arg.PortNumber;
        IpAddress = arg.IpAddress;
        BaseUrl = arg.BaseUrl;
        RateLimitingMax = arg.RateLimitingMax;
        RateLimitingMin = arg.RateLimitingMin;
        AuthenticationWorkflow = arg.AuthenticationWorkflow;
        RulesAndConditions = arg.RulesAndConditions;
        if (arg.ApiTypeId.HasValue) ApiTypeId = new(arg.ApiTypeId.Value);
        if (arg.ApiMethodCallId.HasValue) ApiMethodCallId = new(arg.ApiMethodCallId.Value);
        if (arg.ApiAuthentoicationMethodId.HasValue) ApiAuthentoicationMethodId = new(arg.ApiAuthentoicationMethodId.Value);
        if (arg.NetworkProtocolId.HasValue) NetworkProtocolId = new(arg.NetworkProtocolId.Value);
        if (arg.OwnerResponsibleId.HasValue) OwnerResponsibleId = new(arg.OwnerResponsibleId.Value);
        if (arg.OwnerDepartmentId.HasValue) OwnerDepartmentId = new(arg.OwnerDepartmentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Api> Create(CreateApiArg arg, IApiDomainService service)
    {
        await CreateGuards(arg, service);
        return new Api(arg);
    }
    public async Task Modify(ModifyApiArg arg, IApiDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        Description = arg.Description;
        Prerequisites = arg.Prerequisites;
        ApiAddress = arg.ApiAddress;
        PortNumber = arg.PortNumber;
        IpAddress = arg.IpAddress;
        BaseUrl = arg.BaseUrl;
        RateLimitingMax = arg.RateLimitingMax;
        RateLimitingMin = arg.RateLimitingMin;
        AuthenticationWorkflow = arg.AuthenticationWorkflow;
        RulesAndConditions = arg.RulesAndConditions;
        if (arg.ApiTypeId.HasValue) ApiTypeId = new(arg.ApiTypeId.Value);
        if (arg.ApiMethodCallId.HasValue) ApiMethodCallId = new(arg.ApiMethodCallId.Value);
        if (arg.ApiAuthentoicationMethodId.HasValue) ApiAuthentoicationMethodId = new(arg.ApiAuthentoicationMethodId.Value);
        if (arg.NetworkProtocolId.HasValue) NetworkProtocolId = new(arg.NetworkProtocolId.Value);
        if (arg.OwnerResponsibleId.HasValue) OwnerResponsibleId = new(arg.OwnerResponsibleId.Value);
        if (arg.OwnerDepartmentId.HasValue) OwnerDepartmentId = new(arg.OwnerDepartmentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region Guards
    private static async Task CreateGuards(CreateApiArg arg, IApiDomainService service)
    {

    }
    private static async Task ModifyGuards(ModifyApiArg arg, IApiDomainService service)
    {

    }
    #endregion
    public ApiId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public string? Prerequisites { get; private set; }
    public string? BaseUrl { get; private set; }
    public string? ApiAddress { get; private set; }
    public string? IpAddress { get; private set; }
    public string? PortNumber { get; private set; }
    public int? RateLimitingMin { get; private set; }
    public int? RateLimitingMax { get; private set; }
    public ApiTypeId? ApiTypeId { get; private set; }
    public virtual ApiType? ApiType { get; private set; }
    public ApiMethodCallId? ApiMethodCallId { get; private set; }
    public virtual ApiMethodCall? ApiMethodCall { get; private set; }
    public ApiAuthentoicationMethodId? ApiAuthentoicationMethodId { get; private set; }
    public virtual ApiAuthentoicationMethod? ApiAuthentoicationMethod { get; private set; }
    public NetworkProtocolId? NetworkProtocolId { get; private set; }
    public virtual NetworkProtocol? NetworkProtocol { get; private set; }
    public string? AuthenticationWorkflow { get; private set; }
    public DepartmentId? OwnerDepartmentId { get; private set; }
    public virtual Department? OwnerDepartment { get; private set; }
    public StaffId? OwnerResponsibleId { get; private set; }
    public virtual Staff? OwnerResponsible { get; private set; }
    public string? RulesAndConditions { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<ServiceApi> _serviceApis = new();
    public ICollection<ServiceApi> ServiceApis => _serviceApis;
    private List<ApiSupportTeam> _apiSupportTeams = new();
    public ICollection<ApiSupportTeam> ApiSupportTeams => _apiSupportTeams;
    private List<ApiDocument> _apiDocuments = new();
    public ICollection<ApiDocument> ApiDocuments => _apiDocuments;
    private List<ApiRequestHeaderParam> _apiRequestHeaderParams = new();
    public ICollection<ApiRequestHeaderParam> ApiRequestHeaderParams => _apiRequestHeaderParams;
    private List<ApiVersion> _apiVersions = new();
    public ICollection<ApiVersion> ApiVersions => _apiVersions;
    private List<ApiRequestBodyParam> _apiRequestBodyParams = new();
    public ICollection<ApiRequestBodyParam> ApiRequestBodyParams => _apiRequestBodyParams;
    private List<ApiRequestQueryStringParam> _apiRequestQueryStringParams = new();
    public ICollection<ApiRequestQueryStringParam> ApiRequestQueryStringParams => _apiRequestQueryStringParams;
    private List<ApiRequestUrlParam> _apiRequestUrlParams = new();
    public ICollection<ApiRequestUrlParam> ApiRequestUrlParams => _apiRequestUrlParams;
    private List<ApiResponseBodyParam> _apiResponseBodyParams = new();
    public ICollection<ApiResponseBodyParam> ApiResponseBodyParams => _apiResponseBodyParams;
    private List<ApiResponseHeaderParam> _apiResponseHeaderParams = new();
    public ICollection<ApiResponseHeaderParam> ApiResponseHeaderParams => _apiResponseHeaderParams;
}