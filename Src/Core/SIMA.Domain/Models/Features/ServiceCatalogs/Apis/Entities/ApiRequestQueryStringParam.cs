using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ApiRequestQueryStringParam : Entity
{
    private ApiRequestQueryStringParam() { }
    private ApiRequestQueryStringParam(CreateApiRequestQueryStringParamArg arg)
    {
        Id = new ApiRequestQueryStringParamId(arg.Id);
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
    public static ApiRequestQueryStringParam Create(CreateApiRequestQueryStringParamArg arg)
    {
        return new ApiRequestQueryStringParam(arg);
    }
    public ApiRequestQueryStringParamId Id { get; private set; }
    public ApiId ApiId { get; private set; }
    public virtual Api Api  { get; private set; }
    public string Name { get; private set; }
    public string DataType { get; private set; }
    public string? IsMandatory { get; private set; }
    public string? Description { get; private set; }
    public ApiRequestQueryStringParamId? ParentId { get; private set; }
    public virtual ApiRequestQueryStringParam? Parent { get; private set; }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
