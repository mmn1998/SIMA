using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.Args;
using SIMA.Domain.Models.Features.BCP.Consequences.Contracts;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.Consequences.Entities;

public class Consequence : Entity, IAggregateRoot
{
    private Consequence()
    {

    }
    private Consequence(CreateConsequenceArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Consequence> Create(CreateConsequenceArg arg, IConsequenceDomainService service)
    {
        await CreateGuards(arg, service);
        return new Consequence(arg);
    }
    public async Task Modify(ModifyConsequenceArg arg, IConsequenceDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateConsequenceArg arg, IConsequenceDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyConsequenceArg arg, IConsequenceDomainService service)
    {

    }
    #endregion
    public ConsequenceId Id { get; set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<BusinessImpactAnalysis> _businessImpactAnalyses = new();
    public ICollection<BusinessImpactAnalysis> BusinessImpactAnalyses => _businessImpactAnalyses;
}
