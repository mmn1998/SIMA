using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.Args;
using SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.Contracts;
using SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.Entities;

public class MaximumAcceptableOutage : Entity, IAggregateRoot
{
    private MaximumAcceptableOutage()
    {

    }
    private MaximumAcceptableOutage(CreateMaximumAcceptableOutageArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        DurationHourFrom = arg.DurationHourFrom;
        DurationHourTo = arg.DurationHourTo;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<MaximumAcceptableOutage> Create(CreateMaximumAcceptableOutageArg arg, IMaximumAcceptableOutageDomainService service)
    {
        await CreateGuards(arg, service);
        return new MaximumAcceptableOutage(arg);
    }
    public async Task Modify(ModifyMaximumAcceptableOutageArg arg, IMaximumAcceptableOutageDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        DurationHourFrom = arg.DurationHourFrom;
        DurationHourTo = arg.DurationHourTo;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateMaximumAcceptableOutageArg arg, IMaximumAcceptableOutageDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyMaximumAcceptableOutageArg arg, IMaximumAcceptableOutageDomainService service)
    {

    }
    #endregion
    public MaximumAcceptableOutageId Id { get; set; }
    public int DurationHourFrom { get; set; }
    public int? DurationHourTo { get; set; }
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
