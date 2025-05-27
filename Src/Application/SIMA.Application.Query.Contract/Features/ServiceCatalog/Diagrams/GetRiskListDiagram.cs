using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetRiskListDiagram:  IQuery<Result<List<GetRiskListDiagramResult>>>
{
    public string? Type { get; set; }
    public long? Id { get; set; }
}