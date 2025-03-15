using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Diagrams;

namespace SIMA.Application.Query.Features.ServiceCatalog.Diagrams;

public class ServiceCatalogDiagramQueryHandler : IQueryHandler<GetServiceNetworkDiagramQuery, Result<List<GetServiceNetworkDiagramQueryResult>>>,
    IQueryHandler<GetServiceTreeDiagramQuery, Result<List<GetServiceTreeDiagramQueryResult>>>, IQueryHandler<GetProductList, Result<List<GetProductListResult>>>,
    IQueryHandler<GetChannelList, Result<List<GetChannelListResult>>>,IQueryHandler<GetAssetList, Result<List<GetAssetListResult>>>,
    IQueryHandler<GetAssignedStaffList, Result<List<GetAssignedStaffListResult>>>,IQueryHandler<GetRiskList, Result<List<GetRiskListResult>>>,
    IQueryHandler<GetApiList, Result<List<GetApiListResult>>>,IQueryHandler<GetProcedureList, Result<List<GetProcedureListResult>>>
    
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

    public async Task<Result<List<GetProductListResult>>> Handle(GetProductList request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetProductDiagrams(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetChannelListResult>>> Handle(GetChannelList request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetChannelDiagrams(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetAssetListResult>>> Handle(GetAssetList request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAssetDiagrams(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetAssignedStaffListResult>>> Handle(GetAssignedStaffList request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAssignedStaffList(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetRiskListResult>>> Handle(GetRiskList request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetRiskLis(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetApiListResult>>> Handle(GetApiList request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetApiList(request);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetProcedureListResult>>> Handle(GetProcedureList request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetProcedureList(request);
        return Result.Ok(result);
    }
}
