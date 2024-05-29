using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiVersion : Entity
{
    private ApiVersion() { }
    private ApiVersion(CreateApiVersionArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        ApiId = new(arg.ApiId);
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
    public string? VersionNumber { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string? IsCurrentVersion { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
