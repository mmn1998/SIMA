using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.Entities;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class Api : Entity, IAggregateRoot
{
    private Api() { }
    private Api(CreateApiArg arg)
    {
        Id = new(arg.Id);
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
        IsInternalApi = arg.IsInternalApi;
        if (arg.ApiTypeId.HasValue) ApiTypeId = new(arg.ApiTypeId.Value);
        if (arg.ApiAuthenticationMethodId.HasValue) ApiAuthenticationMethodId = new(arg.ApiAuthenticationMethodId.Value);
        if (arg.ApiMethodActionId.HasValue) ApiMethodActionId = new(arg.ApiMethodActionId.Value);
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
        IsInternalApi = arg.IsInternalApi;
        ApiAddress = arg.ApiAddress;
        PortNumber = arg.PortNumber;
        IpAddress = arg.IpAddress;
        BaseUrl = arg.BaseUrl;
        RateLimitingMax = arg.RateLimitingMax;
        RateLimitingMin = arg.RateLimitingMin;
        AuthenticationWorkflow = arg.AuthenticationWorkflow;
        RulesAndConditions = arg.RulesAndConditions;
        if (arg.ApiTypeId.HasValue) ApiTypeId = new(arg.ApiTypeId.Value);
        if (arg.ApiMethodActionId.HasValue) ApiMethodActionId = new(arg.ApiMethodActionId.Value);
        if (arg.ApiAuthenticationMethodId.HasValue) ApiAuthenticationMethodId = new(arg.ApiAuthenticationMethodId.Value);
        if (arg.NetworkProtocolId.HasValue) NetworkProtocolId = new(arg.NetworkProtocolId.Value);
        if (arg.OwnerResponsibleId.HasValue) OwnerResponsibleId = new(arg.OwnerResponsibleId.Value);
        if (arg.OwnerDepartmentId.HasValue) OwnerDepartmentId = new(arg.OwnerDepartmentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateApiArg arg, IApiDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.IsInternalApi.NullCheck();
        arg.IpAddress.NullCheck();
        
        if (!IPHelper.IsValidIPv4(arg.IpAddress)) throw new SimaResultException(CodeMessges._100116Code, Messages.InvalidIpv4Error);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyApiArg arg, IApiDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.IsInternalApi.NullCheck();
        arg.IpAddress.NullCheck();

        if (!IPHelper.IsValidIPv4(arg.IpAddress)) throw new SimaResultException(CodeMessges._100116Code, Messages.InvalidIpv4Error);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ApiId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string? IsInternalApi { get; private set; }
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
    public NetworkProtocolId? NetworkProtocolId { get; private set; }
    public virtual NetworkProtocol? NetworkProtocol { get; private set; }
    public ApiMethodActionId? ApiMethodActionId { get; private set; }
    public virtual ApiMethodAction? ApiMethodAction { get; private set; }
    public ApiAuthenticationMethodId? ApiAuthenticationMethodId { get; private set; }
    public virtual ApiAuthenticationMethod? ApiAuthenticationMethod { get; private set; }
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
        #region DeleteRelatedEntities
        DeleteApiSupportTeams(userId);
        DeleteConfigurationItemApis(userId);
        DeleteApiRequestHeaderParams(userId);
        DeleteApiResponseHeaderParams(userId);
        DeleteApiRequestBodyParams(userId);
        DeleteApiResponseBodyParams(userId);
        DeleteApiRequestUrlParams(userId);
        DeleteApiRequestQueryStringParams(userId);
        #endregion
    }
    #region Delete Relations
    public void DeleteApiSupportTeams(long userId)
    {
        foreach (var item in _apiSupportTeams)
        {
            item.Delete(userId);
        }
    }
    public void DeleteConfigurationItemApis(long userId)
    {
        foreach (var item in _configurationItemApis)
        {
            item.Delete(userId);
        }
    }
    public void DeleteApiRequestHeaderParams(long userId)
    {
        foreach (var item in _apiRequestHeaderParams)
        {
            item.Delete(userId);
        }
    }
    public void DeleteApiResponseHeaderParams(long userId)
    {
        foreach (var item in _apiResponseHeaderParams)
        {
            item.Delete(userId);
        }
    }
    public void DeleteApiRequestBodyParams(long userId)
    {
        foreach (var item in _apiRequestBodyParams)
        {
            item.Delete(userId);
        }
    }
    public void DeleteApiResponseBodyParams(long userId)
    {
        foreach (var item in _apiResponseBodyParams)
        {
            item.Delete(userId);
        }
    }
    public void DeleteApiRequestUrlParams(long userId)
    {
        foreach (var item in _apiRequestUrlParams)
        {
            item.Delete(userId);
        }
    }
    public void DeleteApiRequestQueryStringParams(long userId)
    {
        foreach (var item in _apiRequestQueryStringParams)
        {
            item.Delete(userId);
        }
    }
    #endregion

    #region Add Methodes
    public void AddApiDocument(List<CreateApiDocumentArg> args, long ApiId, long userId)
    {
        var previousDocument = _apiDocuments.Where(x => x.ApiId == new ApiId(ApiId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addDocument = args.Where(x => !previousDocument.Any(c => c.DocumentId.Value == x.DocumentId)).ToList();
        var deleteDocument = previousDocument.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value)).ToList();

        foreach (var document in addDocument)
        {
            var entity = _apiDocuments.Where(x => (x.DocumentId == new DocumentId(document.DocumentId) && x.ApiId == new ApiId(ApiId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = ApiDocument.Create(document);
                _apiDocuments.Add(entity);
            }
        }

        foreach (var document in deleteDocument)
        {
            document.Delete(userId);
        }
    }
    public void AddApiSupportTeam(List<CreateApiSupportTeamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ApiSupportTeam.Create(arg);
            _apiSupportTeams.Add(entity);
        }
    }
    public void AddConfigurationItemApi(List<CreateConfigurationItemApiArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemApi.Create(arg);
            _configurationItemApis.Add(entity);
        }
    }
    public void AddApiRequestHeaderParam(List<CreateApiRequestHeaderParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ApiRequestHeaderParam.Create(arg);
            _apiRequestHeaderParams.Add(entity);
        }
    }
    public void AddApiRequestBodyParam(List<CreateApiRequestBodyParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ApiRequestBodyParam.Create(arg);
            _apiRequestBodyParams.Add(entity);
        }
    }
    public void AddApiResponseBodyParam(List<CreateApiResponseBodyParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ApiResponseBodyParam.Create(arg);
            _apiResponseBodyParams.Add(entity);
        }
    }
    public void AddApiResponseHeaderParam(List<CreateApiResponseHeaderParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ApiResponseHeaderParam.Create(arg);
            _apiResponseHeaderParams.Add(entity);
        }
    }
    public void AddApiRequestUrlParam(List<CreateApiRequestUrlParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ApiRequestUrlParam.Create(arg);
            _apiRequestUrlParams.Add(entity);
        }
    }
    public void AddApiRequestQueryStringParam(List<CreateApiRequestQueryStringParamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ApiRequestQueryStringParam.Create(arg);
            _apiRequestQueryStringParams.Add(entity);
        }
    }
    #endregion

    #region Modify Methodes
    public void ModifyApiSupportTeam(List<CreateApiSupportTeamArg> args)
    {
        var activeEntities = _apiSupportTeams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _apiSupportTeams.FirstOrDefault(x => x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ApiSupportTeam.Create(arg);
                _apiSupportTeams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemApi(List<CreateConfigurationItemApiArg> args)
    {
        var activeEntities = _configurationItemApis.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ConfigurationItemId == x.ConfigurationItemId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ConfigurationItemId.Value == x.ConfigurationItemId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemApis.FirstOrDefault(x => x.ConfigurationItemId.Value == arg.ConfigurationItemId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemApi.Create(arg);
                _configurationItemApis.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyApiRequestHeaderParam(List<CreateApiRequestHeaderParamArg> args)
    {
        var activeEntities = _apiRequestHeaderParams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _apiRequestHeaderParams.FirstOrDefault(x => x.Name == arg.Name && x.DataType == arg.DataType && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ApiRequestHeaderParam.Create(arg);
                _apiRequestHeaderParams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyApiRequestBodyParam(List<CreateApiRequestBodyParamArg> args)
    {
        var activeEntities = _apiRequestBodyParams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _apiRequestBodyParams.FirstOrDefault(x => x.Name == arg.Name && x.DataType == arg.DataType && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ApiRequestBodyParam.Create(arg);
                _apiRequestBodyParams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyApiResponseBodyParam(List<CreateApiResponseBodyParamArg> args)
    {
        var activeEntities = _apiResponseBodyParams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _apiResponseBodyParams.FirstOrDefault(x => x.Name == arg.Name && x.DataType == arg.DataType && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ApiResponseBodyParam.Create(arg);
                _apiResponseBodyParams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyApiResponseHeaderParam(List<CreateApiResponseHeaderParamArg> args)
    {
        var activeEntities = _apiResponseHeaderParams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _apiResponseHeaderParams.FirstOrDefault(x => x.Name == arg.Name && x.DataType == arg.DataType && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ApiResponseHeaderParam.Create(arg);
                _apiResponseHeaderParams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyApiRequestUrlParam(List<CreateApiRequestUrlParamArg> args)
    {
        var activeEntities = _apiRequestUrlParams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _apiRequestUrlParams.FirstOrDefault(x => x.Name == arg.Name && x.DataType == arg.DataType && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ApiRequestUrlParam.Create(arg);
                _apiRequestUrlParams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyApiRequestQueryStringParam(List<CreateApiRequestQueryStringParamArg> args)
    {
        var activeEntities = _apiRequestQueryStringParams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.Name == x.Name && c.DataType == x.DataType));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _apiRequestQueryStringParams.FirstOrDefault(x => x.Name == arg.Name && x.DataType == arg.DataType && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ApiRequestQueryStringParam.Create(arg);
                _apiRequestQueryStringParams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion

    private List<ServiceApi> _serviceApis = new();
    public ICollection<ServiceApi> ServiceApis => _serviceApis;
    private List<ApiSupportTeam> _apiSupportTeams = new();
    public ICollection<ApiSupportTeam> ApiSupportTeams => _apiSupportTeams;
    private List<ApiDocument> _apiDocuments = new();
    public ICollection<ApiDocument> ApiDocuments => _apiDocuments;

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
    private List<ConfigurationItemApi> _configurationItemApis = new();
    public ICollection<ConfigurationItemApi> ConfigurationItemApis => _configurationItemApis;
    private List<ApiRequestHeaderParam> _apiRequestHeaderParams = new();
    public ICollection<ApiRequestHeaderParam> ApiRequestHeaderParams => _apiRequestHeaderParams;
}