using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiVersion : Entity
{
    private ApiVersion() { }
    private ApiVersion(CreateApiVersionArg arg)
    {
        Id = new ApiVersionId(arg.Id);
        ApiId = new ApiId(arg.ApiId);
        IsCurrentVersion = arg.IsCurrentVersion;
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ApiVersion Create(CreateApiVersionArg arg)
    {
        return new ApiVersion(arg);
    }
    public void Modify(ModifyApiVersionArg arg)
    {
        ApiId = new(arg.ApiId);
        IsCurrentVersion = arg.IsCurrentVersion;
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public ApiVersionId Id { get; private set; }
    public ApiId ApiId { get; private set; }
    public virtual Api Api { get; private set; }
    public string VersionNumber { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string IsCurrentVersion { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    //private List<ApiRequestHeaderParam> _apiRequestHeaderParams = new();
    //public ICollection<ApiRequestHeaderParam> ApiRequestHeaderParams => _apiRequestHeaderParams;
    //private List<ApiRequestBodyParam> _apiRequestBodyParams = new();
    //public ICollection<ApiRequestBodyParam> ApiRequestBodyParams => _apiRequestBodyParams;
    //private List<ApiRequestUrlParam> _apiRequestUrlParams = new();
    //public ICollection<ApiRequestUrlParam> ApiRequestUrlParams => _apiRequestUrlParams;
    //private List<ApiRequestQueryStringParam> _apiRequestQueryStringParams = new();
    //public ICollection<ApiRequestQueryStringParam> ApiRequestQueryStringParams => _apiRequestQueryStringParams;

    //private List<ApiResponseHeaderParam> _apiResponseHeaderParams = new();
    //public ICollection<ApiResponseHeaderParam> ApiResponseHeaderParams => _apiResponseHeaderParams;

    //private List<ApiResponseBodyParam> _apiResponseBodyParams = new();
    //public ICollection<ApiResponseBodyParam> ApiResponseBodyParams => _apiResponseBodyParams;
}
