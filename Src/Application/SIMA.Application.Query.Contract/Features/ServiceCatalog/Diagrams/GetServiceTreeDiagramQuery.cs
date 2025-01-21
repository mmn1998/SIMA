using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetServiceTreeDiagramQuery : IQuery<Result<List<GetServiceTreeDiagramQueryResult>>>
{
    public int? Type { get; set; }
    public long? Id { get; set; }
}