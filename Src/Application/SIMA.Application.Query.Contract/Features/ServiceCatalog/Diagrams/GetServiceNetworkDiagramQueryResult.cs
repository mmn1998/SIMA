namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetServiceNetworkDiagramQueryResult
{
    public List<ServiceNetworkDiagramNode>? Nodes { get; set; }
    public List<ServiceNetworkDiagramEdges>? Edges { get; set; }
}public class GetServiceNetworkDiagramQueryResultWrapper
{
    public List<GetServiceNetworkDiagramQueryResult>? data { get; set; }
}
