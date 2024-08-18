using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiRequestHeaderParam : Entity
{
    private ApiRequestHeaderParam() { }
    private ApiRequestHeaderParam(CreateApiRequestHeaderParamArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        ApiVersionId = new ApiVersionId(arg.ApiVersionId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Name = arg.Name;
        DataType = arg.DataType;
        IsMandatory = arg.IsMandatory;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ApiRequestHeaderParam Create(CreateApiRequestHeaderParamArg arg)
    {
        return new ApiRequestHeaderParam(arg);
    }
    public void Modify(ModifyApiRequestHeaderParamArg arg)
    {
        ApiVersionId = new ApiVersionId(arg.ApiVersionId);
        if (arg.ParentId.HasValue) ParentId = new ApiRequestHeaderParamId(arg.ParentId.Value);
        Name = arg.Name;
        DataType = arg.DataType;
        IsMandatory = arg.IsMandatory;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public ApiRequestHeaderParamId Id { get; private set; }

    public ApiVersionId ApiVersionId { get; private set; }
    public virtual ApiVersion ApiVersion  { get; private set; }
    public string Name { get; private set; }
    public string DataType { get; private set; }
    public string? IsMandatory { get; private set; }
    public string? Description { get; private set; }
    public ApiRequestHeaderParamId? ParentId { get; private set; }
    public virtual ApiRequestHeaderParam? Parent { get; private set; }
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
}
