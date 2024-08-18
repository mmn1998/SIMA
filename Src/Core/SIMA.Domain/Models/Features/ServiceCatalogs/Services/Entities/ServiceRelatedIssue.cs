using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceRelatedIssue : Entity
{
    private ServiceRelatedIssue()
    {
    }
    private ServiceRelatedIssue(CreateServiceRelatedIssueArg arg)
    {
        Id = new ServiceRelatedIssueId(arg.Id);
        ServiceId= new ServiceId(arg.ServiceId);
        IssueId= new IssueId(arg.IssueId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ServiceRelatedIssue> Create(CreateServiceRelatedIssueArg arg)
    {
        return new ServiceRelatedIssue(arg);
    }
    public ServiceRelatedIssueId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public IssueId IssueId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
