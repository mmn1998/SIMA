namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetServiceNetworkDiagramQueryResult
{
    public List<ServiceNetworkDiagramNode>? Nodes { get; set; }
    public List<ServiceNetworkDiagramLink>? Links { get; set; }
}public class GetServiceNetworkDiagramQueryResultWrapper
{
    public List<GetServiceNetworkDiagramQueryResult>? data { get; set; }
}
