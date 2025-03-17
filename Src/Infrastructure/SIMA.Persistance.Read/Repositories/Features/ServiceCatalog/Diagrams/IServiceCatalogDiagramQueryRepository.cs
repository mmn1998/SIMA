
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Diagrams;

public interface IServiceCatalogDiagramQueryRepository : IQueryRepository
{
    Task<List<GetServiceNetworkDiagramQueryResult>> GetNetworkDiagrams(GetServiceNetworkDiagramQuery query);
    Task<List<GetServiceTreeDiagramQueryResult>> GetTreeDiagrams(GetServiceTreeDiagramQuery query);
    Task<List<GetProductListResult>> GetProductDiagrams(GetProductList query);
    Task<List<GetChannelListResult>> GetChannelDiagrams(GetChannelList query);
    Task<List<GetAssetListResult>> GetAssetDiagrams(GetAssetList query);
    Task<List<GetAssignedStaffListResult>> GetAssignedStaffList(GetAssignedStaffList query);
    Task<List<GetApiListResult>> GetApiList(GetApiList query);
    Task<List<GetProcedureListResult>> GetProcedureList(GetProcedureList query);
    Task<List<GetRiskListResult>> GetRiskLis(GetRiskList query);
    Task<List<GetConfigurationItemListResult>> GetConfigurationItemList(GetConfigurationItemList query);
}