using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Args;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Contracts;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;

public class ImportanceDegree : Entity, IAggregateRoot
{
    private ImportanceDegree()
    {

    }
    private ImportanceDegree(CreateImportanceDegreeArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Ordering = arg.Ordering;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ImportanceDegree> Create(CreateImportanceDegreeArg arg, IImportanceDegreeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ImportanceDegree(arg);
    }
    public async Task Modify(ModifyImportanceDegreeArg arg, IImportanceDegreeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Ordering = arg.Ordering;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateImportanceDegreeArg arg, IImportanceDegreeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyImportanceDegreeArg arg, IImportanceDegreeDomainService service)
    {

    }
    #endregion
    public ImportanceDegreeId Id { get; set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }
    public float Ordering { get; private set; }

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
