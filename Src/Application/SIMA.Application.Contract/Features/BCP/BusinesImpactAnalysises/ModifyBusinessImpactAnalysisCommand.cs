using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.BusinesImpactAnalysises;

public class ModifyBusinessImpactAnalysisCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public long ImportanceDegreeId { get; set; }
    public long ServicePriorityId { get; set; }
    public string? RestartReason { get; set; }
    public long BackupPeriodId { get; set; }
    public List<long>? BusinessImpactAnalysisAssetList { get; set; }
    //public List<long>? BusinessImpactAnalysisStaffList { get; set; }
    public List<long>? BusinessImpactAnalysisDocumentList { get; set; }
    public List<CreateBusinessImpactAnalysisDisasterOriginCommand>? BusinessImpactAnalysisDisasterOriginList { get; set; }
}
