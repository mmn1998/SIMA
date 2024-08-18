using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanService : Entity
{
    private BusinessContinuityPlanService() { }
    private BusinessContinuityPlanService(CreateBusinessContinuityPlanServiceArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        ServiceId = new(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanService Create(CreateBusinessContinuityPlanServiceArg arg)
    {
        return new BusinessContinuityPlanService(arg);
    }
    public void Modify(ModifyBusinessContinuityPlanServiceArg arg)
    {
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        ServiceId = new(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessContinuityPlanServiceId Id { get; private set; }
    public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; private set; }
    public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; private set; }
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