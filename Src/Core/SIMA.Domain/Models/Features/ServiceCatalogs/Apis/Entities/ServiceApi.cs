using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;

public class ServiceApi : Entity
{
    private ServiceApi() { }
    private ServiceApi(CreateServiceApiArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        ApiId = new(arg.ApiId);
        ServiceId = new(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ServiceApi Create(CreateServiceApiArg arg)
    {
        return new ServiceApi(arg);
    }
    public void Modify(ModifyServiceApiArg arg)
    {
        ApiId = new(arg.ApiId);
        ServiceId = new(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public ServiceApiId Id { get; private set; }
    public ApiId ApiId { get; private set; }
    public virtual Api Api { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Service Service { get; private set; }
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
}
