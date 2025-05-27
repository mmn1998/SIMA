using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Diagrams;

namespace SIMA.Application.Query.Features.ServiceCatalog.Diagrams;

public class ServiceCatalogDiagramQueryHandler : IQueryHandler<GetServiceNetworkDiagramQuery, Result<List<GetServiceNetworkDiagramQueryResult>>>,
    IQueryHandler<GetServiceTreeDiagramQuery, Result<List<GetServiceTreeDiagramQueryResult>>>, IQueryHandler<GetProductListDiagram, Result<List<GetProductListDiagramResult>>>,
    IQueryHandler<GetChannelListDiagram, Result<List<GetChannelListDiagramResult>>>,IQueryHandler<GetAssetListDiagram, Result<List<GetAssetListDiagramResult>>>,
    IQueryHandler<GetAssignedStaffListDiagram, Result<List<GetAssignedStaffListDiagramResult>>>,IQueryHandler<GetRiskListDiagram, Result<List<GetRiskListDiagramResult>>>,
    IQueryHandler<GetApiListDiagram, Result<List<GetApiListDiagramResult>>>,IQueryHandler<GetProcedureListDiagram, Result<List<GetProcedureListDiagramResult>>>,
    IQueryHandler<GetConfigurationItemListDiagram, Result<List<GetConfigurationItemListDiagramResult>>>
    
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

    public async Task<Result<List<GetProductListDiagramResult>>> Handle(GetProductListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetProductDiagrams(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetChannelListDiagramResult>>> Handle(GetChannelListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetChannelDiagrams(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetAssetListDiagramResult>>> Handle(GetAssetListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAssetDiagrams(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetAssignedStaffListDiagramResult>>> Handle(GetAssignedStaffListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAssignedStaffList(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetRiskListDiagramResult>>> Handle(GetRiskListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetRiskLis(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetApiListDiagramResult>>> Handle(GetApiListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetApiList(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetProcedureListDiagramResult>>> Handle(GetProcedureListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetProcedureList(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetConfigurationItemListDiagramResult>>> Handle(GetConfigurationItemListDiagram request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetConfigurationItemList(request);
        return Result.Ok(result);
    }
}
