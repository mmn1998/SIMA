using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Diagrams;

namespace SIMA.Application.Query.Features.ServiceCatalog.Diagrams;

public class ServiceCatalogDiagramQueryHandler : IQueryHandler<GetServiceNetworkDiagramQuery, Result<List<GetServiceNetworkDiagramQueryResult>>>,
    IQueryHandler<GetServiceTreeDiagramQuery, Result<List<GetServiceTreeDiagramQueryResult>>>
{
    private readonly IServiceCatalogDiagramQueryRepository _repository;

    public ServiceCatalogDiagramQueryHandler(IServiceCatalogDiagramQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetServiceNetworkDiagramQueryResult>>> Handle(GetServiceNetworkDiagramQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetNetworkDiagrams(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetServiceTreeDiagramQueryResult>>> Handle(GetServiceTreeDiagramQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetTreeDiagrams(request);
        return Result.Ok(result);
    }
}
