using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiResponseBodyParam : Entity
{
    private ApiResponseBodyParam() { }
    private ApiResponseBodyParam(CreateApiResponseBodyParamArg arg)
    {
        Id = new ApiResponseBodyParamId(arg.Id);
        ApiVersionId = new ApiVersionId(arg.ApiVersionId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Name = arg.Name;
        DataType = arg.DataType;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ApiResponseBodyParam Create(CreateApiResponseBodyParamArg arg)
    {
        return new ApiResponseBodyParam(arg);
    }
    public void Modify(ModifyApiResponseBodyParamArg arg)
    {
        ApiVersionId = new ApiVersionId(arg.ApiVersionId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Name = arg.Name;
        DataType = arg.DataType;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public ApiResponseBodyParamId Id { get; private set; }
    public ApiVersionId ApiVersionId { get; private set; }
    public virtual ApiVersion ApiVersion  { get; private set; }
    public string Name { get; private set; }
    public string DataType { get; private set; }
    public string? Description { get; private set; }
    public ApiResponseBodyParamId? ParentId { get; private set; }
    public virtual ApiResponseBodyParam? Parent { get; private set; }
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
