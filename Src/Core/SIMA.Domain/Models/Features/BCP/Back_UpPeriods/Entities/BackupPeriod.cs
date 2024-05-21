using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Args;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Contracts;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;

public class BackupPeriod : Entity, IAggregateRoot
{
    private BackupPeriod()
    {

    }
    private BackupPeriod(CreateBackupPeriodArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BackupPeriod> Create(CreateBackupPeriodArg arg, IBackupPeriodDomainService service)
    {
        await CreateGuards(arg, service);
        return new BackupPeriod(arg);
    }
    public async Task Modify(ModifyBackupPeriodArg arg, IBackupPeriodDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateBackupPeriodArg arg, IBackupPeriodDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyBackupPeriodArg arg, IBackupPeriodDomainService service)
    {

    }
    #endregion
    public BackupPeriodId Id { get; set; }
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
