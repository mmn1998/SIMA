using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStrategyService : Entity
{
    private BusinessContinuityStrategyService() { }
    private BusinessContinuityStrategyService(CreateBusinessContinuityStrategyServiceArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        //ServiceId = new(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityStrategyService Create(CreateBusinessContinuityStrategyServiceArg arg)
    {
        return new BusinessContinuityStrategyService(arg);
    }
    public void Modify(ModifyBusinessContinuityStrategyServiceArg arg)
    {
        BusinessContinuityStategyId = new(arg.BusinessContinuityStategyId);
        //ServiceId = new(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public BusinessContinuityStrategyServiceId Id { get; set; }
    public BusinessContinuityStrategyId BusinessContinuityStategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStategy { get; private set; }
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