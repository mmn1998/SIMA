using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiRequestBodyParam : Entity
{
    private ApiRequestBodyParam() { }
    private ApiRequestBodyParam(CreateApiRequestBodyParamArg arg)
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
    public static ApiRequestBodyParam Create(CreateApiRequestBodyParamArg arg)
    {
        return new ApiRequestBodyParam(arg);
    }
    public void Modify(ModifyApiRequestBodyParamArg arg)
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
    public ApiRequestBodyParamId Id { get; private set; }
    public ApiId ApiId { get; private set; }
    public virtual Api Api { get; private set; }
    public string? Name { get; private set; }
    public string? DataType { get; private set; }
    public string? IsMandatory { get; private set; }
    public string? Description { get; private set; }
    public ApiRequestBodyParamId? ParentId { get; private set; }
    public virtual ApiRequestBodyParam? Parent { get; private set; }
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
