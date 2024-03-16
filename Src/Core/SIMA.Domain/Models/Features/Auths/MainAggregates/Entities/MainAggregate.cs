using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Args;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;

public class MainAggregate
{
    private MainAggregate() { }
    private MainAggregate(CreateMainAggregateArg arg)
    {
        Id = new MainAggregateId(IdHelper.GenerateUniqueId());
        DomainId = new DomainId(arg.DomainId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ModifiedBy = arg.ModifiedBy;
    }
    public static async Task<MainAggregate> Create(CreateMainAggregateArg arg)
    {
        return new MainAggregate(arg);
    }
    public MainAggregateId Id { get; private set; }

    public DomainId DomainId { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Domains.Entities.Domain Domain { get; private set; } = null!;
    public ICollection<Document> Documents { get; private set; }
    public ICollection<Issue> Issues { get; private set; }
    public ICollection<WorkFlow> WorkFlows { get; private set; }
}
