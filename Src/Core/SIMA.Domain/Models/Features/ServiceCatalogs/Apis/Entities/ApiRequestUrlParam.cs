using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiRequestUrlParam : Entity
{
    private ApiRequestUrlParam() { }
    private ApiRequestUrlParam(CreateApiRequestUrlParamArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        ApiId = new(arg.ApiId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Name = arg.Name;
        DataType = arg.DataType;
        IsMandatory = arg.IsMandatory;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ApiRequestUrlParam Create(CreateApiRequestUrlParamArg arg)
    {
        return new ApiRequestUrlParam(arg);
    }
    public void Modify(ModifyApiRequestUrlParamArg arg)
    {
        ApiId = new(arg.ApiId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Name = arg.Name;
        DataType = arg.DataType;
        IsMandatory = arg.IsMandatory;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public ApiRequestUrlParamId Id { get; private set; }
    public ApiId ApiId { get; private set; }
    public virtual Api Api { get; private set; }
    public string? Name { get; private set; }
    public string? DataType { get; private set; }
    public string? IsMandatory { get; private set; }
    public string? Description { get; private set; }
    public ApiRequestUrlParamId? ParentId { get; private set; }
    public virtual ApiRequestUrlParam? Parent { get; private set; }
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
