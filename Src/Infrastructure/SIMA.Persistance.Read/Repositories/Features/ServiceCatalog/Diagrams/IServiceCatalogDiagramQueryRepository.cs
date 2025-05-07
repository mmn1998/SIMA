
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Diagrams;

public interface IServiceCatalogDiagramQueryRepository : IQueryRepository
{
    Task<List<GetServiceNetworkDiagramQueryResult>> GetNetworkDiagrams(GetServiceNetworkDiagramQuery query);
    Task<List<GetServiceTreeDiagramQueryResult>> GetTreeDiagrams(GetServiceTreeDiagramQuery query);
    Task<List<GetProductListDiagramResult>> GetProductDiagrams(GetProductListDiagram query);
    Task<List<GetChannelListDiagramResult>> GetChannelDiagrams(GetChannelListDiagram query);
    Task<List<GetAssetListDiagramResult>> GetAssetDiagrams(GetAssetListDiagram query);
    Task<List<GetAssignedStaffListDiagramResult>> GetAssignedStaffList(GetAssignedStaffListDiagram query);
    Task<List<GetApiListDiagramResult>> GetApiList(GetApiListDiagram query);
    Task<List<GetProcedureListDiagramResult>> GetProcedureList(GetProcedureListDiagram query);
    Task<List<GetRiskListDiagramResult>> GetRiskLis(GetRiskListDiagram query);
    Task<List<GetConfigurationItemListDiagramResult>> GetConfigurationItemList(GetConfigurationItemListDiagram query);
}