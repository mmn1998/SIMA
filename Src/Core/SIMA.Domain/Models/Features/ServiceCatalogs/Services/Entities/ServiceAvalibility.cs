using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceAvalibility : Entity
{
    private ServiceAvalibility()
    {
    }
    private ServiceAvalibility(CreateServiceAvalibilityArg arg)
    {
        Id = new(arg.Id);
        ServiceId = new(arg.ServiceId);
        WeekDay = arg.WeekDay;
        ServiceAvalibilityEndTime = arg.ServiceAvalibilityEndTime;
        ServiceAvalibilityStartTime = arg.ServiceAvalibilityStartTime;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ServiceAvalibility Create(CreateServiceAvalibilityArg arg)
    {
        return new ServiceAvalibility(arg);
    }
    public ServiceAvalibilityId Id { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Service Service { get; private set; }
    public int WeekDay { get; private set; }
    public TimeOnly ServiceAvalibilityStartTime { get; private set; }
    public TimeOnly ServiceAvalibilityEndTime { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ModifiedBy = userId;
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ModifiedBy = userId;
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
