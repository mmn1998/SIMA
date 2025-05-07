using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

public class BusinessContinuityStrategyIssue : Entity
{
    private BusinessContinuityStrategyIssue()
    {
        
    }
    private BusinessContinuityStrategyIssue(CreateBusinessContinuityStrategyIssueArg arg)
    {
        Id = new(arg.Id);
        IssueId = new(arg.IssueId);
        BusinessContinuityStrategyId = new(arg.BusinessContinuityStategyId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }  
    public static BusinessContinuityStrategyIssue Create(CreateBusinessContinuityStrategyIssueArg arg)
    {
        return new BusinessContinuityStrategyIssue(arg);
    }
    public BusinessContinuityStrategyIssueId Id { get; private set; }
    public BusinessContinuityStrategyId BusinessContinuityStrategyId { get; private set; }
    public virtual BusinessContinuityStrategy BusinessContinuityStrategy { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
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
