using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityService : Entity
{
    private CriticalActivityService() { }
    private CriticalActivityService(CreateCriticalActivityServicesArg arg)
    {
        Id = new CriticalActivityServiceId(arg.Id);
        CriticalActivityId = new CriticalActivityId(arg.CriticalActivityId);
        ServiceId = new ServiceId(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static CriticalActivityService Create(CreateCriticalActivityServicesArg arg)
    {
        return new CriticalActivityService(arg);
    }

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

    public CriticalActivityServiceId Id { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
